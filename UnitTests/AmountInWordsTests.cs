using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiExample.Enums;
using WebApiExample.Features.AmountInWords;
using WebApiExample.SharedServices.NumberInWords;

namespace UnitTests
{
    [TestClass]
    public class AmountInWordsTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly Mock<ICurrencyCzechNameRepository> _currencyCzechNameRepository;

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

        public AmountInWordsTests()
        {
            var services = new ServiceCollection();

            _currencyCzechNameRepository = new();
            services.AddSingleton(_currencyCzechNameRepository.Object);

            services.AddTransient<INumberInWordsCzechService, NumberInWordsCzechService>();
            services.AddTransient<IAmountInWordsCzechService, AmountInWordsCzechService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        private void SetupMockRepository(string currencyCode, CurrencyCzechName mockObject)
        {
            _currencyCzechNameRepository
                .Setup(repo => repo.GetCurrencyCzechNameByCodeAsync(currencyCode))
                .ReturnsAsync(mockObject);
        }

        [TestMethod]
        public async Task AmountToWords_InvalidCurrencyCode()
            => Assert.IsTrue((await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(50, "cz")).Equals(string.Empty));

        [TestMethod]
        public async Task AmountToWords_CurrencyCodeCaps()
        {
            string currencyCode = "CZK";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("jedna koruna", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(1, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_OneCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("jedna koruna", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(1, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_TwoCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("dvě koruny", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(2, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_FiveCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("pět korun", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(5, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_ElevenCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("jedenáct korun", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(11, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_TwelveCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("dvanáct korun", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(12, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_TwentyOneCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("dvacet jedna koruna", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(21, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_TwentyTwoCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("dvacet dvě koruny", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(22, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction1CZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("sto třicet pět korun jeden haléř", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(135.01m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction2CZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("sto třicet pět korun dva haléře", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(135.02m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction3CZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("sto třicet pět korun devadesát devět haléřů", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(135.99m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction3CZKNoSpaces()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("stotřicetpět korun devadesátdevět haléřů", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(135.99m, currencyCode, false));
        }

        [TestMethod]
        public async Task AmountToWords_NegativeCZK()
        {
            string currencyCode = "czk";
            SetupMockRepository(currencyCode, mockCzk);
            Assert.AreEqual("minus čtyřicet dvě koruny osmdesát čtyři haléře", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(-42.84m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction1EUR()
        {
            string currencyCode = "eur";
            SetupMockRepository(currencyCode, mockEur);
            Assert.AreEqual("jedno euro jeden cent", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(1.01m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction2EUR()
        {
            string currencyCode = "eur";
            SetupMockRepository(currencyCode, mockEur);
            Assert.AreEqual("třicet dvě eura čtyřicet tři centy", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(32.43m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction3EUR()
        {
            string currencyCode = "eur";
            SetupMockRepository(currencyCode, mockEur);
            Assert.AreEqual("sto devadesát devět eur devadesát devět centů", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(199.99m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction1USD()
        {
            string currencyCode = "usd";
            SetupMockRepository(currencyCode, mockUsd);
            Assert.AreEqual("jeden dolar jeden cent", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(1.01m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction2USD()
        {
            string currencyCode = "usd";
            SetupMockRepository(currencyCode, mockUsd);
            Assert.AreEqual("třicet dva dolary čtyřicet tři centy", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(32.43m, currencyCode));
        }

        [TestMethod]
        public async Task AmountToWords_Fraction3USD()
        {
            string currencyCode = "usd";
            SetupMockRepository(currencyCode, mockUsd);
            Assert.AreEqual("sto devadesát devět dolarů devadesát devět centů", await _serviceProvider.GetService<IAmountInWordsCzechService>().AmountToWordsAsync(199.99m, currencyCode));
        }
    }
}
