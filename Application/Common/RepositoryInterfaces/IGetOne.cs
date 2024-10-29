namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to select one row from a DB table specified by <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IGetOne<T> where T : Model
    {
        /// <summary>
        /// Selects one row from a DB table specified by <see cref="T"/> based on the ID.
        /// </summary>
        /// <param name="id">ID of the row.</param>
        /// <returns>Selected model if the ID was found; otherwise, null.</returns>
        Task<T?> GetOneAsync(Guid id);
    }
}
