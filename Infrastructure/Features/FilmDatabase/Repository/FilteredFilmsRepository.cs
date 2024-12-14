using Application.Features.FilmDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.FilmDatabase.Repository
{
    /// <inheritdoc cref="IFilteredFilmsRepository"/>
    public class FilteredFilmsRepository(ApplicationDbContext dbContext) : IFilteredFilmsRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<List<FilmModelForList>> GetFilteredFilms(string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null)
        {
            return await _dbContext.Films.Select(f => new FilmModelForList
            {
                Id = f.Id,
                Name = f.Name,
                YearOfRelease = f.YearOfRelease,
                LengthInMinutes = f.LengthInMinutes,
                Rating = f.Rating
            })
                .Where(f => nameContains == null || EF.Functions.Like(f.Name, $"%{nameContains}%"))
                .Where(f => minYearOfRelease == null || f.YearOfRelease >= minYearOfRelease)
                .Where(f => maxYearOfRelease == null || f.YearOfRelease <= maxYearOfRelease)
                .Where(f => minLength == null || f.LengthInMinutes >= minLength)
                .Where(f => maxLength == null || f.LengthInMinutes <= maxLength)
                .Where(f => minRating == null || f.Rating >= minRating)
                .Where(f => maxRating == null || f.Rating <= maxRating)
                .ToListAsync();
        }

    }
}
