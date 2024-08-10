﻿using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Features.FilmDatabase.V1
{
    public class Film : Entity
    {
        [StringLength(Constants.FilmNameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required short YearOfRelease { get; set; }

        public required short LengthInMinutes { get; set; }

        public required byte Rating { get; set; }

        public required string ImagePath { get; set; }
    }
}