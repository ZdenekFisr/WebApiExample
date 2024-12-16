using Application.Features.AmountToWords;
using Application.Features.NumberToWords;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Features.AmountToWords.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.UnitTests.FeatureTests.AmountInWords
{
    public class AmountInWordsCzechServiceTests
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly Mock<INumberToWordsCzechService> _numberInWordsCzechService;
        private readonly AmountInWordsCzechService _amountInWordsCzechService;

        public AmountInWordsCzechServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            _context = new ApplicationDbContext(options);

            _numberInWordsCzechService = new Mock<INumberToWordsCzechService>();

            var embeddedCsvService = new EmbeddedCsvService();
            var currencyCzechNameRepository = new CurrencyCzechNameRepository(_context, _mapper);
            _amountInWordsCzechService = new(_numberInWordsCzechService.Object, currencyCzechNameRepository);

            var currencyCzechNames = embeddedCsvService
                .ReadEmbeddedCsv<CurrencyCzechName>("Infrastructure.UnitTests.FeatureTests.AmountInWords.CurrencyCzechNames.csv");

            _context.CurrencyCzechNames.AddRange(currencyCzechNames);
            _context.SaveChanges();
        }

        private async Task PerformAmountToWordsTestAsync(
            string expected, decimal input,
            string currencyCode,
            GrammaticalGender genderWholePart, string mockWholePart,
            GrammaticalGender? genderFractionPart = null, string? mockFractionPart = null)
        {
            long wholePart = (long)Math.Truncate(input);

            _numberInWordsCzechService
                .Setup(service => service.NumberToWords(wholePart, genderWholePart, true))
                .Returns(mockWholePart);
            if (genderFractionPart is not null && mockFractionPart is not null)
            {
                _numberInWordsCzechService
                    .Setup(service => service.NumberToWords(Math.Abs((long)((input - wholePart) * 100)), genderFractionPart.Value, true))
                    .Returns(mockFractionPart);
            }

            string actual = await _amountInWordsCzechService.AmountToWordsAsync(input, currencyCode, true);

            actual.Should().BeEquivalentTo(expected);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AmountToWords_InvalidCurrencyCode()
            => await PerformAmountToWordsTestAsync(string.Empty, 50, "cz", GrammaticalGender.Feminine, "padesát");

        [Fact]
        public async Task AmountToWords_OneCZK()
            => await PerformAmountToWordsTestAsync("jedna koruna", 1, "czk", GrammaticalGender.Feminine, "jedna");

        [Fact]
        public async Task AmountToWords_TwoCZK()
            => await PerformAmountToWordsTestAsync("dvě koruny", 2, "czk", GrammaticalGender.Feminine, "dvě");

        [Fact]
        public async Task AmountToWords_FiveCZK()
            => await PerformAmountToWordsTestAsync("pět korun", 5, "czk", GrammaticalGender.Feminine, "pět");

        [Fact]
        public async Task AmountToWords_ElevenCZK()
            => await PerformAmountToWordsTestAsync("jedenáct korun", 11, "czk", GrammaticalGender.Feminine, "jedenáct");

        [Fact]
        public async Task AmountToWords_TwelveCZK()
            => await PerformAmountToWordsTestAsync("dvanáct korun", 12, "czk", GrammaticalGender.Feminine, "dvanáct");

        [Fact]
        public async Task AmountToWords_TwentyOneCZK()
            => await PerformAmountToWordsTestAsync("dvacet jedna koruna", 21, "czk", GrammaticalGender.Feminine, "dvacet jedna");

        [Fact]
        public async Task AmountToWords_TwentyTwoCZK()
            => await PerformAmountToWordsTestAsync("dvacet dvě koruny", 22, "czk", GrammaticalGender.Feminine, "dvacet dvě");

        [Fact]
        public async Task AmountToWords_Fraction1CZK()
            => await PerformAmountToWordsTestAsync("sto třicet pět korun jeden haléř", 135.01m, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "jeden");

        [Fact]
        public async Task AmountToWords_Fraction2CZK()
            => await PerformAmountToWordsTestAsync("sto třicet pět korun dva haléře", 135.02m, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "dva");

        [Fact]
        public async Task AmountToWords_Fraction3CZK()
            => await PerformAmountToWordsTestAsync("sto třicet pět korun devadesát devět haléřů", 135.99m, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "devadesát devět");

        [Fact]
        public async Task AmountToWords_Fraction3CZKNoSpaces()
            => await PerformAmountToWordsTestAsync("stotřicetpět korun devadesátdevět haléřů", 135.99m, "czk", GrammaticalGender.Feminine, "stotřicetpět", GrammaticalGender.Masculine, "devadesátdevět");

        [Fact]
        public async Task AmountToWords_NegativeCZK()
            => await PerformAmountToWordsTestAsync("minus čtyřicet dvě koruny osmdesát čtyři haléře", -42.84m, "czk", GrammaticalGender.Feminine, "minus čtyřicet dvě", GrammaticalGender.Masculine, "osmdesát čtyři");

        [Fact]
        public async Task AmountToWords_Fraction1EUR()
            => await PerformAmountToWordsTestAsync("jedno euro jeden cent", 1.01m, "eur", GrammaticalGender.Neuter, "jedno", GrammaticalGender.Masculine, "jeden");

        [Fact]
        public async Task AmountToWords_Fraction2EUR()
            => await PerformAmountToWordsTestAsync("třicet dvě eura čtyřicet tři centy", 32.43m, "eur", GrammaticalGender.Neuter, "třicet dvě", GrammaticalGender.Masculine, "čtyřicet tři");

        [Fact]
        public async Task AmountToWords_Fraction3EUR()
            => await PerformAmountToWordsTestAsync("sto devadesát devět eur devadesát devět centů", 199.99m, "eur", GrammaticalGender.Neuter, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");

        [Fact]
        public async Task AmountToWords_Fraction1USD()
            => await PerformAmountToWordsTestAsync("jeden dolar jeden cent", 1.01m, "usd", GrammaticalGender.Masculine, "jeden", GrammaticalGender.Masculine, "jeden");

        [Fact]
        public async Task AmountToWords_Fraction2USD()
            => await PerformAmountToWordsTestAsync("třicet dva dolary čtyřicet tři centy", 32.43m, "usd", GrammaticalGender.Masculine, "třicet dva", GrammaticalGender.Masculine, "čtyřicet tři");

        [Fact]
        public async Task AmountToWords_Fraction3USD()
            => await PerformAmountToWordsTestAsync("sto devadesát devět dolarů devadesát devět centů", 199.99m, "usd", GrammaticalGender.Masculine, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");
    }
}
