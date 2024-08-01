using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiExample;
using WebApiExample.Features.FilmDatabase;

namespace UnitTests
{
    [TestClass]
    public class FilteredFilmsTests
    {
        private ServiceProvider _serviceProvider;

        private readonly List<Film> _inMemoryDbFilms =
        [
            new()
            {
                Name = "Gladiator",
                Description = "American film directed by Ridley Scott.",
                YearOfRelease = 2000,
                LengthInMinutes = 155,
                Rating = 89,
                ImagePath = string.Empty
            },
            new()
            {
                Name = "Der Untergang",
                Description = "German film about the last days of WW2 in Berlin. English name: \"Downfall\"",
                YearOfRelease = 2004,
                LengthInMinutes = 156,
                Rating = 82,
                ImagePath = string.Empty
            },
            new()
            {
                Name = "Bod obnovy",
                Description = "Czech sci-fi film. English name: \"Restore point\".",
                YearOfRelease = 2023,
                LengthInMinutes = 116,
                Rating = 70,
                ImagePath = string.Empty
            },
            new()
            {
                Name = "Interstellar",
                Description = "American sci-fi film directed by Christopher Nolan.",
                YearOfRelease = 2014,
                LengthInMinutes = 169,
                Rating = 85,
                ImagePath = string.Empty
            },
            new()
            {
                Name = "The Shawshank Redemption",
                Description = "American film taking place in prison.",
                YearOfRelease = 1994,
                LengthInMinutes = 142,
                Rating = 95,
                ImagePath = string.Empty
            }
        ];

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<IFilteredFilmsRepository, FilteredFilmsRepository>();

            _serviceProvider = services.BuildServiceProvider();

            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Films.AddRange(_inMemoryDbFilms);
            context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.EnsureDeleted();
        }

        private async Task<List<string>> GetFilteredNames(string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var repository = scope.ServiceProvider.GetRequiredService<IFilteredFilmsRepository>();

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