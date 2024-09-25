using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiExample.Enums;
using WebApiExample.Features.AmountInWords.V1;
using WebApiExample.SharedServices.NumberInWords;

namespace UnitTests
{
    [TestClass]
    public class AmountInWordsTests
    {
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        private Mock<ICurrencyCzechNameRepository> _currencyCzechNameRepository;
        private Mock<INumberInWordsCzechService> _numberInWordsCzechService;

        private readonly CurrencyCzechName mockCzk = new()
        {
            Code = "czk",
            OneUnit = "koruna",
            TwoToFourUnits = "koruny",
            FiveOrMoreUnits = "korun",
            UnitGrammaticalGender = GrammaticalGender.Feminine,
            OneSubunit = "haléř",
            TwoToFourSubunits = "haléře",
            FiveOrMoreSubunits = "haléřů",
            SubunitGrammaticalGender = GrammaticalGender.Masculine
        };
        private readonly CurrencyCzechName mockEur = new()
        {
            Code = "eur",
            OneUnit = "euro",
            TwoToFourUnits = "eura",
            FiveOrMoreUnits = "eur",
            UnitGrammaticalGender = GrammaticalGender.Neuter,
            OneSubunit = "cent",
            TwoToFourSubunits = "centy",
            FiveOrMoreSubunits = "centů",
            SubunitGrammaticalGender = GrammaticalGender.Masculine
        };
        private readonly CurrencyCzechName mockUsd = new()
        {
            Code = "usd",
            OneUnit = "dolar",
            TwoToFourUnits = "dolary",
            FiveOrMoreUnits = "dolarů",
            UnitGrammaticalGender = GrammaticalGender.Masculine,
            OneSubunit = "cent",
            TwoToFourSubunits = "centy",
            FiveOrMoreSubunits = "centů",
            SubunitGrammaticalGender = GrammaticalGender.Masculine
        };

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            _currencyCzechNameRepository = new();
            services.AddSingleton(_currencyCzechNameRepository.Object);
            _numberInWordsCzechService = new();
            services.AddSingleton(_numberInWordsCzechService.Object);

            services.AddTransient<IAmountInWordsCzechService, AmountInWordsCzechService>();

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceScope.Dispose();
            _serviceProvider.Dispose();
        }

        private async Task PerformTestAsync(
            decimal input, string expected,
            string currencyCode, CurrencyCzechName mockCurrencyCzechName,
            GrammaticalGender genderWholePart, string mockWholePart,
            GrammaticalGender? genderFractionPart = null, string? mockFractionPart = null)
        {
            long wholePart = (long)Math.Truncate(input);

            _currencyCzechNameRepository
                .Setup(repo => repo.GetCurrencyCzechNameByCodeAsync(currencyCode))
                .ReturnsAsync(mockCurrencyCzechName);
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

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task AmountToWords_InvalidCurrencyCode()
            => Assert.IsTrue((await _serviceProvider.GetRequiredService<IAmountInWordsCzechService>().AmountToWordsAsync(50, "cz")).Equals(string.Empty));

        [TestMethod]
        public async Task AmountToWords_CurrencyCodeCaps()
            => await PerformTestAsync(1, "jedna koruna", "CZK", mockCzk, GrammaticalGender.Feminine, "jedna");

        [TestMethod]
        public async Task AmountToWords_OneCZK()
            => await PerformTestAsync(1, "jedna koruna", "czk", mockCzk, GrammaticalGender.Feminine, "jedna");

        [TestMethod]
        public async Task AmountToWords_TwoCZK()
            => await PerformTestAsync(2, "dvě koruny", "czk", mockCzk, GrammaticalGender.Feminine, "dvě");

        [TestMethod]
        public async Task AmountToWords_FiveCZK()
            => await PerformTestAsync(5, "pět korun", "czk", mockCzk, GrammaticalGender.Feminine, "pět");

        [TestMethod]
        public async Task AmountToWords_ElevenCZK()
            => await PerformTestAsync(11, "jedenáct korun", "czk", mockCzk, GrammaticalGender.Feminine, "jedenáct");

        [TestMethod]
        public async Task AmountToWords_TwelveCZK()
            => await PerformTestAsync(12, "dvanáct korun", "czk", mockCzk, GrammaticalGender.Feminine, "dvanáct");

        [TestMethod]
        public async Task AmountToWords_TwentyOneCZK()
            => await PerformTestAsync(21, "dvacet jedna koruna", "czk", mockCzk, GrammaticalGender.Feminine, "dvacet jedna");

        [TestMethod]
        public async Task AmountToWords_TwentyTwoCZK()
            => await PerformTestAsync(22, "dvacet dvě koruny", "czk", mockCzk, GrammaticalGender.Feminine, "dvacet dvě");

        [TestMethod]
        public async Task AmountToWords_Fraction1CZK()
            => await PerformTestAsync(135.01m, "sto třicet pět korun jeden haléř", "czk", mockCzk, GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2CZK()
            => await PerformTestAsync(135.02m, "sto třicet pět korun dva haléře", "czk", mockCzk, GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "dva");

        [TestMethod]
        public async Task AmountToWords_Fraction3CZK()
            => await PerformTestAsync(135.99m, "sto třicet pět korun devadesát devět haléřů", "czk", mockCzk, GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "devadesát devět");

        [TestMethod]
        public async Task AmountToWords_Fraction3CZKNoSpaces()
            => await PerformTestAsync(135.99m, "stotřicetpět korun devadesátdevět haléřů", "czk", mockCzk, GrammaticalGender.Feminine, "stotřicetpět", GrammaticalGender.Masculine, "devadesátdevět");

        [TestMethod]
        public async Task AmountToWords_NegativeCZK()
            => await PerformTestAsync(-42.84m, "minus čtyřicet dvě koruny osmdesát čtyři haléře", "czk", mockCzk, GrammaticalGender.Feminine, "minus čtyřicet dvě", GrammaticalGender.Masculine, "osmdesát čtyři");

        [TestMethod]
        public async Task AmountToWords_Fraction1EUR()
            => await PerformTestAsync(1.01m, "jedno euro jeden cent", "eur", mockEur, GrammaticalGender.Neuter, "jedno", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2EUR()
            => await PerformTestAsync(32.43m, "třicet dvě eura čtyřicet tři centy", "eur", mockEur, GrammaticalGender.Neuter, "třicet dvě", GrammaticalGender.Masculine, "čtyřicet tři");

        [TestMethod]
        public async Task AmountToWords_Fraction3EUR()
            => await PerformTestAsync(199.99m, "sto devadesát devět eur devadesát devět centů", "eur", mockEur, GrammaticalGender.Neuter, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");

        [TestMethod]
        public async Task AmountToWords_Fraction1USD()
            => await PerformTestAsync(1.01m, "jeden dolar jeden cent", "usd", mockUsd, GrammaticalGender.Masculine, "jeden", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2USD()
            => await PerformTestAsync(32.43m, "třicet dva dolary čtyřicet tři centy", "usd", mockUsd, GrammaticalGender.Masculine, "třicet dva", GrammaticalGender.Masculine, "čtyřicet tři");

        [TestMethod]
        public async Task AmountToWords_Fraction3USD()
            => await PerformTestAsync(199.99m, "sto devadesát devět dolarů devadesát devět centů", "usd", mockUsd, GrammaticalGender.Masculine, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");
    }
}
