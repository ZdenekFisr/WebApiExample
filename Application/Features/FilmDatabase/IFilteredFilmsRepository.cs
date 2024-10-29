namespace Application.Features.FilmDatabase
{
    /// <summary>
    /// Contains a method to select filtered rows from the DB table "Films".
    /// </summary>
    public interface IFilteredFilmsRepository
    {
        /// <summary>
        /// Selects rows from the DB table "Films" that meet all the conditions. If the condition is null, this filter is not applied.
        /// </summary>
        /// <param name="nameContains">Get films whose name contains this substring. Case is ignored.</param>
        /// <param name="minYearOfRelease">Get films whose year of release is greater or equal to this value.</param>
        /// <param name="maxYearOfRelease">Get films whose year of release is lower or equal to this value.</param>
        /// <param name="minLength">Get films whose length in minutes is greater or equal to this value.</param>
        /// <param name="maxLength">Get films whose length in minutes is lower or equal to this value.</param>
        /// <param name="minRating">Get films whose rating in percent is greater or equal to this value.</param>
        /// <param name="maxRating">Get films whose rating in percent is lower or equal to this value.</param>
        /// <returns>A list of films that meet all the conditions.</returns>
        Task<List<FilmModelForList>> GetFilteredFilms(string? nameContains = null, short? minYearOfRelease = null, short? maxYearOfRelease = null, short? minLength = null, short? maxLength = null, byte? minRating = null, byte? maxRating = null);
    }
}
