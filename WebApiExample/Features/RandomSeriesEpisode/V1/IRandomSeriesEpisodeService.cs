namespace WebApiExample.Features.RandomSeriesEpisode.V1
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
        public Episode Generate(IEnumerable<int> numbersOfEpisodes);
    }
}
