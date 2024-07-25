using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Features.FilmDatabase
{
    public class FilmModel : Model
    {
        [StringLength(Constants.FilmMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required short YearOfRelease { get; set; }

        public required short LengthInMinutes { get; set; }

        public required byte Rating { get; set; }

        public required string ImageUrl { get; set; }
    }
}
