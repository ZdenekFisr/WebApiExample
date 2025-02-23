using Application.Features.FilmDatabase;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Features.FilmDatabase.V1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/film-database")]
    [ApiController]
    public class FilmDatabaseController(
        IFilteredFilmsRepository repository)
        : ControllerBase
    {
        private readonly IFilteredFilmsRepository _repository = repository;

        [HttpGet]
        [EndpointDescription("Gets all films that match the specified criteria.")]
        public async Task<IActionResult> GetFilteredFilms(string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null)
            => Ok(await _repository.GetFilteredFilms(nameContains, minYearOfRelease, maxYearOfRelease, minLength, maxLength, minRating, maxRating));
    }
}
