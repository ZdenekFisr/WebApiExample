using Application.Features.AmountToWords;
using Application.Features.NumberToWords;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Features.AmountToWords.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.UnitTests.FeaturesTests.AmountInWords
{
    public class AmountInWordsCzechServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<INumberToWordsCzechService> _numberInWordsCzechService;
        private readonly AmountInWordsCzechService _amountInWordsCzechService;

        public AmountInWordsCzechServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _numberInWordsCzechService = new Mock<INumberToWordsCzechService>();

            var embeddedCsvService = new EmbeddedCsvService();
            var currencyCzechNameRepository = new CurrencyCzechNameRepository(_context);
            _amountInWordsCzechService = new(_numberInWordsCzechService.Object, currencyCzechNameRepository);

            var currencyCzechNames = embeddedCsvService
                .ReadEmbeddedCsv<CurrencyCzechName>("Infrastructure.UnitTests.FeaturesTests.AmountInWords.CurrencyCzechNames.csv");

            _context.CurrencyCzechNames.AddRange(currencyCzechNames);
            _context.SaveChanges();
        }

        [Theory]
        [InlineData("", 50, "cz", GrammaticalGender.Feminine, "padesát")] // invalid currency code
        [InlineData("jedna koruna", 1, "czk", GrammaticalGender.Feminine, "jedna")]
        [InlineData("dvě koruny", 2, "czk", GrammaticalGender.Feminine, "dvě")]
        [InlineData("pět korun", 5, "czk", GrammaticalGender.Feminine, "pět")]
        [InlineData("jedenáct korun", 11, "czk", GrammaticalGender.Feminine, "jedenáct")]
        [InlineData("dvanáct korun", 12, "czk", GrammaticalGender.Feminine, "dvanáct")]
        [InlineData("dvacet jedna koruna", 21, "czk", GrammaticalGender.Feminine, "dvacet jedna")]
        [InlineData("dvacet dvě koruny", 22, "czk", GrammaticalGender.Feminine, "dvacet dvě")]
        [InlineData("sto třicet pět korun jeden haléř", 135.01, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "jeden")]
        [InlineData("sto třicet pět korun dva haléře", 135.02, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "dva")]
        [InlineData("sto třicet pět korun devadesát devět haléřů", 135.99, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "devadesát devět")]
        [InlineData("stotřicetpět korun devadesátdevět haléřů", 135.99, "czk", GrammaticalGender.Feminine, "stotřicetpět", GrammaticalGender.Masculine, "devadesátdevět")]
        [InlineData("minus čtyřicet dvě koruny osmdesát čtyři haléře", -42.84, "czk", GrammaticalGender.Feminine, "minus čtyřicet dvě", GrammaticalGender.Masculine, "osmdesát čtyři")]
        [InlineData("jedno euro jeden cent", 1.01, "eur", GrammaticalGender.Neuter, "jedno", GrammaticalGender.Masculine, "jeden")]
        [InlineData("třicet dvě eura čtyřicet tři centy", 32.43, "eur", GrammaticalGender.Neuter, "třicet dvě", GrammaticalGender.Masculine, "čtyřicet tři")]
        [InlineData("sto devadesát devět eur devadesát devět centů", 199.99, "eur", GrammaticalGender.Neuter, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět")]
        [InlineData("jeden dolar jeden cent", 1.01, "usd", GrammaticalGender.Masculine, "jeden", GrammaticalGender.Masculine, "jeden")]
        [InlineData("třicet dva dolary čtyřicet tři centy", 32.43, "usd", GrammaticalGender.Masculine, "třicet dva", GrammaticalGender.Masculine, "čtyřicet tři")]
        [InlineData("sto devadesát devět dolarů devadesát devět centů", 199.99, "usd", GrammaticalGender.Masculine, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět")]
        public async Task AmountToWordsAsync_ReturnsCorrectStringRepresentation(
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
    }
}
