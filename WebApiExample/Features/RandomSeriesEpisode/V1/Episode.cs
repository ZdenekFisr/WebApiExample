namespace WebApiExample.Features.RandomSeriesEpisode.V1
{
    public class Episode : Model
    {
        public int SeasonNumber { get; init; }

        public int EpisodeNumber { get; init; }
    }
}
