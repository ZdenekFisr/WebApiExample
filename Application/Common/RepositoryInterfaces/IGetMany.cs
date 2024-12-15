namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to select multiple rows from a DB table specified by <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IGetMany<T> where T : ModelBase
    {
        /// <summary>
        /// Selects multiple rows from a DB table specified by <see cref="T"/>
        /// </summary>
        /// <returns>A collection of models.</returns>
        Task<ICollection<T>> GetManyAsync();
    }
}
