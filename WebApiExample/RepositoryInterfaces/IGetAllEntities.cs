namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to select all rows from a DB table specified by <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IGetAllEntities<T> where T : Entity
    {
        /// <summary>
        /// Selects all rows from a DB table specified by <see cref="T"/>
        /// </summary>
        /// <returns>IEnumerable collection of entities.</returns>
        Task<List<T>> GetAllAsync();
    }
}
