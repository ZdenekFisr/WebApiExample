using Application.Features.FilmDatabase;
using FluentAssertions;
using Infrastructure.Features.FilmDatabase.Repository;
using Infrastructure.Services;

namespace Infrastructure.IntegrationTests.FeaturesTests.FilmDatabase
{
    [Collection("Database")]
    public class FilteredFilmsTests : IAsyncLifetime
    {
        private readonly Func<Task> _resetDatabase;

        private readonly ApplicationDbContext _context;
        private readonly EmbeddedCsvService _csvService;
        private readonly FilteredFilmsRepository _repository;

        public FilteredFilmsTests(DatabaseFixture databaseFixture)
        {
            _resetDatabase = databaseFixture.ResetDatabase;

            _context = databaseFixture.Context;
            _csvService = new();
            _repository = new(_context);
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync() => await _resetDatabase();

        private async Task PerformFilmFilterTest(IEnumerable<string> expected, string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null)
        {
            List<FilmModel> films = _csvService.ReadEmbeddedCsv<FilmModel>("Infrastructure.IntegrationTests.Films.csv");
            await _context.Films.AddRangeAsync(films.Select(f => f.ToEntity()));
            await _context.SaveChangesAsync();

            string[] actual = (await _repository
                .GetFilteredFilms(nameContains, minYearOfRelease, maxYearOfRelease, minLength, maxLength, minRating, maxRating))
                .Select(f => f.Name)
                .ToArray();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task NoFilters()
            => await PerformFilmFilterTest(["Bod obnovy", "Der Untergang", "Gladiator", "Interstellar", "The Shawshank Redemption"]);

        [Fact]
        public async Task FilterByName()
            => await PerformFilmFilterTest(["Der Untergang"], nameContains: "unter");

        [Fact]
        public async Task FilterByMinYearOfRelease()
            => await PerformFilmFilterTest(["Bod obnovy", "Interstellar"], minYearOfRelease: 2014);

        [Fact]
        public async Task FilterByMaxYearOfRelease()
            => await PerformFilmFilterTest(["Gladiator", "The Shawshank Redemption"], maxYearOfRelease: 2000);

        [Fact]
        public async Task FilterByYearOfRelease()
            => await PerformFilmFilterTest(["Der Untergang", "Gladiator"], minYearOfRelease: 2000, maxYearOfRelease: 2010);

        [Fact]
        public async Task FilterByYearOfReleaseBadInput()
            => await PerformFilmFilterTest([], minYearOfRelease: 2010, maxYearOfRelease: 2000);

        [Fact]
        public async Task FilterByMinLength()
            => await PerformFilmFilterTest(["Der Untergang", "Gladiator", "Interstellar"], minLength: 155);

        [Fact]
        public async Task FilterByMaxLength()
            => await PerformFilmFilterTest(["Bod obnovy", "The Shawshank Redemption"], maxLength: 142);

        [Fact]
        public async Task FilterByLength()
            => await PerformFilmFilterTest(["The Shawshank Redemption"], minLength: 140, maxLength: 150);

        [Fact]
        public async Task FilterByMinRating()
            => await PerformFilmFilterTest(["Gladiator", "The Shawshank Redemption"], minRating: 89);

        [Fact]
        public async Task FilterByMaxRating()
            => await PerformFilmFilterTest(["Bod obnovy", "Der Untergang"], maxRating: 82);

        [Fact]
        public async Task FilterByRating()
            => await PerformFilmFilterTest(["Der Untergang", "Gladiator", "Interstellar"], minRating: 80, maxRating: 90);

        [Fact]
        public async Task AllFilters()
            => await PerformFilmFilterTest(["Bod obnovy"], nameContains: "d", minYearOfRelease: 2000, maxYearOfRelease: 2024, minLength: 90, maxLength: 150, minRating: 70, maxRating: 85);
    }
}