namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to select all rows with a given user ID from a DB table specified by <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IGetAllWithUser<T> where T : Model
    {
        /// <summary>
        /// Selects all rows from a DB table with a given user ID specified by <see cref="T"/>
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns>IEnumerable collection of models.</returns>
        Task<List<T>> GetAllAsync(string userId);
    }
}
