using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
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
