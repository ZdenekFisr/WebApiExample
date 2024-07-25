namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to select all rows from a DB table specified by <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IGetAllModels<T> where T : Model
    {
        /// <summary>
        /// Selects all rows from a DB table specified by <see cref="T"/>
        /// </summary>
        /// <returns>IEnumerable collection of models.</returns>
        Task<List<T>> GetAllAsync();
    }
}
