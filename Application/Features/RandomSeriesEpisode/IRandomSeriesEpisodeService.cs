namespace Application.Features.RandomSeriesEpisode
{
    /// <summary>
    /// Contains a method to generate a random episode of a series.
    /// </summary>
    public interface IRandomSeriesEpisodeService
    {
        /// <summary>
        /// Generate a random episode of a series.
        /// </summary>
        /// <param name="numbersOfEpisodes">Numbers of episodes for each season.</param>
        /// <returns>Season number and episode number.</returns>
        Episode Generate(IEnumerable<int> numbersOfEpisodes);
    }
}
