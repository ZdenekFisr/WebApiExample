using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiExample;
using WebApiExample.Enums;
using WebApiExample.Features.AmountInWords.V1;
using WebApiExample.SharedServices.Csv;
using WebApiExample.SharedServices.NumberInWords;

namespace UnitTests.AmountInWordsTests
{
    [TestClass]
    public class AmountInWordsTests
    {
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        private Mock<INumberInWordsCzechService> _numberInWordsCzechService;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            _numberInWordsCzechService = new();
            services.AddSingleton(_numberInWordsCzechService.Object);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped<ICurrencyCzechNameRepository, CurrencyCzechNameRepository>();
            services.AddScoped<IAmountInWordsCzechService, AmountInWordsCzechService>();

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();

            var currencyCzechNames = _serviceProvider
                .GetRequiredService<ICsvService>()
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

        private async Task PerformTestAsync(
            decimal input, string expected,
            string currencyCode,
            GrammaticalGender genderWholePart, string mockWholePart,
            GrammaticalGender? genderFractionPart = null, string? mockFractionPart = null)
        {
            long wholePart = (long)Math.Truncate(input);

            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CurrencyCzechName currencyCzechName = context.CurrencyCzechNames.First(ccn => ccn.Code == currencyCode);

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
        public async Task AmountToWords_OneCZK()
            => await PerformTestAsync(1, "jedna koruna", "czk", GrammaticalGender.Feminine, "jedna");

        [TestMethod]
        public async Task AmountToWords_TwoCZK()
            => await PerformTestAsync(2, "dvě koruny", "czk", GrammaticalGender.Feminine, "dvě");

        [TestMethod]
        public async Task AmountToWords_FiveCZK()
            => await PerformTestAsync(5, "pět korun", "czk", GrammaticalGender.Feminine, "pět");

        [TestMethod]
        public async Task AmountToWords_ElevenCZK()
            => await PerformTestAsync(11, "jedenáct korun", "czk", GrammaticalGender.Feminine, "jedenáct");

        [TestMethod]
        public async Task AmountToWords_TwelveCZK()
            => await PerformTestAsync(12, "dvanáct korun", "czk", GrammaticalGender.Feminine, "dvanáct");

        [TestMethod]
        public async Task AmountToWords_TwentyOneCZK()
            => await PerformTestAsync(21, "dvacet jedna koruna", "czk", GrammaticalGender.Feminine, "dvacet jedna");

        [TestMethod]
        public async Task AmountToWords_TwentyTwoCZK()
            => await PerformTestAsync(22, "dvacet dvě koruny", "czk", GrammaticalGender.Feminine, "dvacet dvě");

        [TestMethod]
        public async Task AmountToWords_Fraction1CZK()
            => await PerformTestAsync(135.01m, "sto třicet pět korun jeden haléř", "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2CZK()
            => await PerformTestAsync(135.02m, "sto třicet pět korun dva haléře", "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "dva");

        [TestMethod]
        public async Task AmountToWords_Fraction3CZK()
            => await PerformTestAsync(135.99m, "sto třicet pět korun devadesát devět haléřů", "czk", GrammaticalGender.Feminine, "sto třicet pět", GrammaticalGender.Masculine, "devadesát devět");

        [TestMethod]
        public async Task AmountToWords_Fraction3CZKNoSpaces()
            => await PerformTestAsync(135.99m, "stotřicetpět korun devadesátdevět haléřů", "czk", GrammaticalGender.Feminine, "stotřicetpět", GrammaticalGender.Masculine, "devadesátdevět");

        [TestMethod]
        public async Task AmountToWords_NegativeCZK()
            => await PerformTestAsync(-42.84m, "minus čtyřicet dvě koruny osmdesát čtyři haléře", "czk", GrammaticalGender.Feminine, "minus čtyřicet dvě", GrammaticalGender.Masculine, "osmdesát čtyři");

        [TestMethod]
        public async Task AmountToWords_Fraction1EUR()
            => await PerformTestAsync(1.01m, "jedno euro jeden cent", "eur", GrammaticalGender.Neuter, "jedno", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2EUR()
            => await PerformTestAsync(32.43m, "třicet dvě eura čtyřicet tři centy", "eur", GrammaticalGender.Neuter, "třicet dvě", GrammaticalGender.Masculine, "čtyřicet tři");

        [TestMethod]
        public async Task AmountToWords_Fraction3EUR()
            => await PerformTestAsync(199.99m, "sto devadesát devět eur devadesát devět centů", "eur", GrammaticalGender.Neuter, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");

        [TestMethod]
        public async Task AmountToWords_Fraction1USD()
            => await PerformTestAsync(1.01m, "jeden dolar jeden cent", "usd", GrammaticalGender.Masculine, "jeden", GrammaticalGender.Masculine, "jeden");

        [TestMethod]
        public async Task AmountToWords_Fraction2USD()
            => await PerformTestAsync(32.43m, "třicet dva dolary čtyřicet tři centy", "usd", GrammaticalGender.Masculine, "třicet dva", GrammaticalGender.Masculine, "čtyřicet tři");

        [TestMethod]
        public async Task AmountToWords_Fraction3USD()
            => await PerformTestAsync(199.99m, "sto devadesát devět dolarů devadesát devět centů", "usd", GrammaticalGender.Masculine, "sto devadesát devět", GrammaticalGender.Masculine, "devadesát devět");
    }
}
