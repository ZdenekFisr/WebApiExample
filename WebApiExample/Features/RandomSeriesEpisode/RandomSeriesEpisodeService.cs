using WebApiExample.SharedServices.RandomNumber;

namespace WebApiExample.Features.RandomSeriesEpisode
{
    /// <inheritdoc cref="IRandomSeriesEpisodeService"/>
    public class RandomSeriesEpisodeService(IRandomNumberService randomNumberService) : IRandomSeriesEpisodeService
    {
        private readonly IRandomNumberService _randomNumberService = randomNumberService;

        /// <inheritdoc />
        public Episode Generate(IEnumerable<int> numbersOfEpisodes)
        {
            int season = _randomNumberService.GenerateRandomInteger(1, numbersOfEpisodes.Count());
            int episode = _randomNumberService.GenerateRandomInteger(1, numbersOfEpisodes.ElementAt(season - 1));

            return new() { SeasonNumber = season, EpisodeNumber = episode };
        }
    }
}
