namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to select multiple rows with a given user ID from a DB table specified by <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IGetManyWithUser<T> where T : ModelBase
    {
        /// <summary>
        /// Selects multiple rows from a DB table with a given user ID specified by <see cref="T"/>
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns>IEnumerable collection of models.</returns>
        Task<ICollection<T>> GetManyAsync(string userId);
    }
}
