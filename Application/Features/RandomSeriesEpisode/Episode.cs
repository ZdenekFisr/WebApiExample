﻿using Application.Common;

namespace Application.Features.RandomSeriesEpisode
{
    public class Episode : Model
    {
        public int SeasonNumber { get; init; }

        public int EpisodeNumber { get; init; }
    }
}
