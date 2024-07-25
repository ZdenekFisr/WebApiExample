using Microsoft.AspNetCore.Mvc;
using WebApiExample.Extensions;

namespace WebApiExample.Features.RandomSeriesEpisode
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomSeriesEpisodeController(IRandomSeriesEpisodeService coreService) : ControllerBase
    {
        private readonly IRandomSeriesEpisodeService _coreService = coreService;

        [HttpGet]
        [EndpointDescription("Accepts a collection of whole numbers separated by semicolons that represent the number of episodes for each season of a show, and returns a randomly generated episode.")]
        public IActionResult GetRandomEpisode(string numbersOfEpisodes)
            => Ok(_coreService.Generate(numbersOfEpisodes.ToIntCollection(';', allowZero: false, allowNegative: false)));
    }
}
