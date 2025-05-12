using Application.Common;
using Domain;
using Domain.Entities;
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

        /// <summary>
        /// Creates a new instance of <see cref="FilmModel"/> from a <see cref="Film"/> entity.
        /// </summary>
        /// <param name="entity">The <see cref="Film"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="FilmModel"/>.</returns>
        public static FilmModel FromEntity(Film entity)
        {
            return new FilmModel
            {
                Name = entity.Name,
                Description = entity.Description,
                YearOfRelease = entity.YearOfRelease,
                LengthInMinutes = entity.LengthInMinutes,
                Rating = entity.Rating,
                ImageUrl = $"{Constants.FilmImageBaseUrl}{entity.ImagePath}"
            };
        }

        /// <summary>
        /// Converts the current <see cref="FilmModel"/> instance to a <see cref="Film"/> entity.
        /// </summary>
        /// <returns>A new instance of <see cref="Film"/>.</returns>
        public Film ToEntity()
        {
            return new Film
            {
                Name = Name,
                Description = Description,
                YearOfRelease = YearOfRelease,
                LengthInMinutes = LengthInMinutes,
                Rating = Rating,
                ImagePath = ImageUrl.Replace(Constants.FilmImageBaseUrl, string.Empty)
            };
        }
    }
}
