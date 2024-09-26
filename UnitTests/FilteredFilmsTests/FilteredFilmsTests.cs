using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiExample;
using WebApiExample.Features.FilmDatabase.V1;
using WebApiExample.SharedServices.Csv;

namespace UnitTests.FilteredFilmsTests
{
    [TestClass]
    public class FilteredFilmsTests
    {
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped<IFilteredFilmsRepository, FilteredFilmsRepository>();

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();

            var films = _serviceProvider
                .GetRequiredService<ICsvService>()
                .ReadEmbeddedCsv<Film>("UnitTests.FilteredFilmsTests.Films.csv");

            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Films.AddRange(films);
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

        private async Task<List<string>> GetFilteredNames(string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null)
        {
            var context = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var repository = _serviceScope.ServiceProvider.GetRequiredService<IFilteredFilmsRepository>();

            return (await repository
                .GetFilteredFilms(nameContains, minYearOfRelease, maxYearOfRelease, minLength, maxLength, minRating, maxRating))
                .Select(f => f.Name)
                .ToList();
        }

        [TestMethod]
        public async Task NoFilters()
            => CollectionAssert.AreEquivalent(new List<string> { "Bod obnovy", "Der Untergang", "Gladiator", "Interstellar", "The Shawshank Redemption" }, await GetFilteredNames());

        [TestMethod]
        public async Task FilterByName()
            => CollectionAssert.AreEquivalent(new List<string> { "Der Untergang" }, await GetFilteredNames(nameContains: "unter"));

        [TestMethod]
        public async Task FilterByMinYearOfRelease()
            => CollectionAssert.AreEquivalent(new List<string> { "Bod obnovy", "Interstellar" }, await GetFilteredNames(minYearOfRelease: 2014));

        [TestMethod]
        public async Task FilterByMaxYearOfRelease()
            => CollectionAssert.AreEquivalent(new List<string> { "Gladiator", "The Shawshank Redemption" }, await GetFilteredNames(maxYearOfRelease: 2000));

        [TestMethod]
        public async Task FilterByYearOfRelease()
            => CollectionAssert.AreEquivalent(new List<string> { "Der Untergang", "Gladiator" }, await GetFilteredNames(minYearOfRelease: 2000, maxYearOfRelease: 2010));

        [TestMethod]
        public async Task FilterByYearOfReleaseBadInput()
            => CollectionAssert.AreEquivalent(new List<string>(), await GetFilteredNames(minYearOfRelease: 2010, maxYearOfRelease: 2000));

        [TestMethod]
        public async Task FilterByMinLength()
            => CollectionAssert.AreEquivalent(new List<string> { "Der Untergang", "Gladiator", "Interstellar" }, await GetFilteredNames(minLength: 155));

        [TestMethod]
        public async Task FilterByMaxLength()
            => CollectionAssert.AreEquivalent(new List<string> { "Bod obnovy", "The Shawshank Redemption" }, await GetFilteredNames(maxLength: 142));

        [TestMethod]
        public async Task FilterByLength()
            => CollectionAssert.AreEquivalent(new List<string> { "The Shawshank Redemption" }, await GetFilteredNames(minLength: 140, maxLength: 150));

        [TestMethod]
        public async Task FilterByMinRating()
            => CollectionAssert.AreEquivalent(new List<string> { "Gladiator", "The Shawshank Redemption" }, await GetFilteredNames(minRating: 89));

        [TestMethod]
        public async Task FilterByMaxRating()
            => CollectionAssert.AreEquivalent(new List<string> { "Bod obnovy", "Der Untergang" }, await GetFilteredNames(maxRating: 82));

        [TestMethod]
        public async Task FilterByRating()
            => CollectionAssert.AreEquivalent(new List<string> { "Der Untergang", "Gladiator", "Interstellar" }, await GetFilteredNames(minRating: 80, maxRating: 90));

        [TestMethod]
        public async Task AllFilters()
            => CollectionAssert.AreEquivalent(new List<string> { "Bod obnovy" }, await GetFilteredNames(nameContains: "d", minYearOfRelease: 2000, maxYearOfRelease: 2024, minLength: 90, maxLength: 150, minRating: 70, maxRating: 85));
    }
}