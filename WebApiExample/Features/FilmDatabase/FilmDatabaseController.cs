using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;
using WebApiExample.GenericRepositories.SimpleModel;

namespace WebApiExample.Features.FilmDatabase
{
    [Route("api/[controller]")]
    public class FilmDatabaseController(
        ISimpleModelRepository<FilmModel> repository,
        IFilteredFilmsRepository filterRepository)
        : SimpleModelController<FilmModel>(repository)
    {
        private readonly IFilteredFilmsRepository _filterRepository = filterRepository;

        [HttpGet]
        public async Task<IActionResult> GetFilteredFilms(string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null)
            => Ok(await _filterRepository.GetFilteredFilms(nameContains, minYearOfRelease, maxYearOfRelease, minLength, maxLength, minRating, maxRating));
    }
}
