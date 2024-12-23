﻿using Application.Common;
using Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.FilmDatabase
{
    public class FilmModel : ModelBase
    {
        [StringLength(Constants.FilmNameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required short YearOfRelease { get; set; }

        [Range(1, short.MaxValue)]
        public required short LengthInMinutes { get; set; }

        [Range(0, 100)]
        public required byte Rating { get; set; }

        public required string ImageUrl { get; set; }
    }
}
