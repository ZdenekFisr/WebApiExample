using Application.Features.FilmDatabase;
using Application.GenericRepositories;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.GenericControllers;

namespace WebApiExample.Features.FilmDatabase.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
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
