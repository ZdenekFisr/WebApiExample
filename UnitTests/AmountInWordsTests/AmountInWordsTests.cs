using Application.Features.AmountToWords;
using Application.Features.NumberToWords;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace UnitTests.AmountInWordsTests
{
    [TestClass]
    public class AmountInWordsTests
    {
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        private Mock<INumberToWordsCzechService> _numberInWordsCzechService;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            _numberInWordsCzechService = new();
            services.AddSingleton(_numberInWordsCzechService.Object);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddAutoMapper(typeof(AmountInWordsTests), typeof(AutoMapperProfile));
            services.AddScoped<IEmbeddedCsvService, EmbeddedCsvService>();
            services.AddScoped<ICurrencyCzechNameRepository, CurrencyCzechNameRepository>();
            services.AddScoped<IAmountInWordsCzechService, AmountInWordsCzechService>();

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();

            var currencyCzechNames = _serviceProvider
                .GetRequiredService<IEmbeddedCsvService>()
                .ReadEmbeddedCsv<CurrencyCzechName>("UnitTests.AmountInWordsTests.CurrencyCzechNames.csv");

            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.CurrencyCzechNames.AddRange(currencyCzechNames);
            context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureDeleted();

            _serviceScope.Dispose();
            _serviceProvider.Dispose();
        }

        private async Task PerformAmountToWordsTestAsync(
            string expected, decimal input,
            string currencyCode,
            GrammaticalGender genderWholePart, string mockWholePart,
            GrammaticalGender? genderFractionPart = null, string? mockFractionPart = null)
        {
            long wholePart = (long)Math.Truncate(input);

            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();

            _numberInWordsCzechService
                .Setup(service => service.NumberToWords(wholePart, genderWholePart, true))
                .Returns(mockWholePart);
            if (genderFractionPart is not null && mockFractionPart is not null)
            {
                _numberInWordsCzechService
                    .Setup(service => service.NumberToWords(Math.Abs((long)((input - wholePart) * 100)), genderFractionPart.Value, true))
                    .Returns(mockFractionPart);
            }

            string actual = await _serviceProvider.GetRequiredService<IAmountInWordsCzechService>().AmountToWordsAsync(input, currencyCode);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task AmountToWords_InvalidCurrencyCode()
            => await PerformAmountToWordsTestAsync(string.Empty, 50, "cz", GrammaticalGender.Feminine, "padesát");

        [TestMethod]
        public async Task AmountToWords_OneCZK()
            => await PerformAmountToWordsTestAsync("jedna koruna", 1, "czk", GrammaticalGender.Feminine, "jedna");

        [TestMethod]
        public async Task AmountToWords_TwoCZK()
            => await PerformAmountToWordsTestAsync("dvě koruny", 2, "czk", GrammaticalGender.Feminine, "dvě");

        [TestMethod]
        public async Task AmountToWords_FiveCZK()
            => await PerformAmountToWordsTestAsync("pět korun", 5, "czk", GrammaticalGender.Feminine, "pět");

        [TestMethod]
        public async Task AmountToWords_ElevenCZK()
            => await PerformAmountToWordsTestAsync("jedenáct korun", 11, "czk", GrammaticalGender.Feminine, "jedenáct");

        [TestMethod]
        public async Task AmountToWords_TwelveCZK()
            => await PerformAmountToWordsTestAsync("dvanáct korun", 12, "czk", GrammaticalGender.Feminine, "dvanáct");

        [TestMethod]
        public async Task AmountToWords_TwentyOneCZK()
            => await PerformAmountToWordsTestAsync("dvacet jedna koruna", 21, "czk", GrammaticalGender.Feminine, "dvacet jedna");

        [TestMethod]
        public async Task AmountToWords_TwentyTwoCZK()
            => await PerformAmountToWordsTestAsync("dvacet dvě koruny", 22, "czk", GrammaticalGender.Feminine, "dvacet dvě");

        [TestMethod]
        public async Task AmountToWords_Fraction1CZK()
            => await PerformAmountToWordsTestAsync("sto třicet pět korun jeden haléř", 135.01m, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2CZK()
            => await PerformAmountToWordsTestAsync("sto třicet pět korun dva haléře", 135.02m, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "dva");

        [TestMethod]
        public async Task AmountToWords_Fraction3CZK()
            => await PerformAmountToWordsTestAsync("sto třicet pět korun devadesát devět haléřů", 135.99m, "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "devadesát devět");

        [TestMethod]
        public async Task AmountToWords_Fraction3CZKNoSpaces()
            => await PerformAmountToWordsTestAsync("stotřicetpět korun devadesátdevět haléřů", 135.99m, "czk", GrammaticalGender.Feminine, "stotřicetpět", GrammaticalGender.Masculine, "devadesátdevět");

        [TestMethod]
        public async Task AmountToWords_NegativeCZK()
            => await PerformAmountToWordsTestAsync("minus čtyřicet dvě koruny osmdesát čtyři haléře", -42.84m, "czk", GrammaticalGender.Feminine, "minus čtyřicet dvě", GrammaticalGender.Masculine, "osmdesát čtyři");

        [TestMethod]
        public async Task AmountToWords_Fraction1EUR()
            => await PerformAmountToWordsTestAsync("jedno euro jeden cent", 1.01m, "eur", GrammaticalGender.Neuter, "jedno", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2EUR()
            => await PerformAmountToWordsTestAsync("třicet dvě eura čtyřicet tři centy", 32.43m, "eur", GrammaticalGender.Neuter, "třicet dvě", GrammaticalGender.Masculine, "čtyřicet tři");

        [TestMethod]
        public async Task AmountToWords_Fraction3EUR()
            => await PerformAmountToWordsTestAsync("sto devadesát devět eur devadesát devět centů", 199.99m, "eur", GrammaticalGender.Neuter, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");

        [TestMethod]
        public async Task AmountToWords_Fraction1USD()
            => await PerformAmountToWordsTestAsync("jeden dolar jeden cent", 1.01m, "usd", GrammaticalGender.Masculine, "jeden", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2USD()
            => await PerformAmountToWordsTestAsync("třicet dva dolary čtyřicet tři centy", 32.43m, "usd", GrammaticalGender.Masculine, "třicet dva", GrammaticalGender.Masculine, "čtyřicet tři");

        [TestMethod]
        public async Task AmountToWords_Fraction3USD()
            => await PerformAmountToWordsTestAsync("sto devadesát devět dolarů devadesát devět centů", 199.99m, "usd", GrammaticalGender.Masculine, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");
    }
}
