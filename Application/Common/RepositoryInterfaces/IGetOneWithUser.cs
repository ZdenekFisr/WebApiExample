namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to select one row from a DB table specified by <see cref="T"/>. For better security, user ID is also required.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IGetOneWithUser<T> where T : ModelBase
    {
        /// <summary>
        /// Selects one row from a DB table specified by <see cref="T"/> based on the ID. For better security, user ID is also required.
        /// </summary>
        /// <param name="id">ID of the row.</param>
        /// <param name="userId">ID of the user.</param>
        /// <returns>Selected model if the ID and the user ID was found; otherwise, null.</returns>
        Task<T?> GetOneAsync(Guid id, string userId);
    }
}
