using Application.Features.FilmDatabase;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IEmbeddedCsvService, EmbeddedCsvService>();
            services.AddScoped<IFilteredFilmsRepository, FilteredFilmsRepository>();

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();

            var films = _serviceProvider
                .GetRequiredService<IEmbeddedCsvService>()
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

        private async Task PerformFilmFilterTest(IEnumerable<string> expected, string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null)
        {
            var context = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var repository = _serviceScope.ServiceProvider.GetRequiredService<IFilteredFilmsRepository>();

            string[] actual = (await repository
                .GetFilteredFilms(nameContains, minYearOfRelease, maxYearOfRelease, minLength, maxLength, minRating, maxRating))
                .Select(f => f.Name)
                .ToArray();

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NoFilters()
            => await PerformFilmFilterTest(["Bod obnovy", "Der Untergang", "Gladiator", "Interstellar", "The Shawshank Redemption"]);

        [TestMethod]
        public async Task FilterByName()
            => await PerformFilmFilterTest(["Der Untergang"], nameContains: "unter");

        [TestMethod]
        public async Task FilterByMinYearOfRelease()
            => await PerformFilmFilterTest(["Bod obnovy", "Interstellar"], minYearOfRelease: 2014);

        [TestMethod]
        public async Task FilterByMaxYearOfRelease()
            => await PerformFilmFilterTest(["Gladiator", "The Shawshank Redemption"], maxYearOfRelease: 2000);

        [TestMethod]
        public async Task FilterByYearOfRelease()
            => await PerformFilmFilterTest(["Der Untergang", "Gladiator"], minYearOfRelease: 2000, maxYearOfRelease: 2010);

        [TestMethod]
        public async Task FilterByYearOfReleaseBadInput()
            => await PerformFilmFilterTest([], minYearOfRelease: 2010, maxYearOfRelease: 2000);

        [TestMethod]
        public async Task FilterByMinLength()
            => await PerformFilmFilterTest(["Der Untergang", "Gladiator", "Interstellar"], minLength: 155);

        [TestMethod]
        public async Task FilterByMaxLength()
            => await PerformFilmFilterTest(["Bod obnovy", "The Shawshank Redemption"], maxLength: 142);

        [TestMethod]
        public async Task FilterByLength()
            => await PerformFilmFilterTest(["The Shawshank Redemption"], minLength: 140, maxLength: 150);

        [TestMethod]
        public async Task FilterByMinRating()
            => await PerformFilmFilterTest(["Gladiator", "The Shawshank Redemption"], minRating: 89);

        [TestMethod]
        public async Task FilterByMaxRating()
            => await PerformFilmFilterTest(["Bod obnovy", "Der Untergang"], maxRating: 82);

        [TestMethod]
        public async Task FilterByRating()
            => await PerformFilmFilterTest(["Der Untergang", "Gladiator", "Interstellar"], minRating: 80, maxRating: 90);

        [TestMethod]
        public async Task AllFilters()
            => await PerformFilmFilterTest(["Bod obnovy"], nameContains: "d", minYearOfRelease: 2000, maxYearOfRelease: 2024, minLength: 90, maxLength: 150, minRating: 70, maxRating: 85);
    }
}