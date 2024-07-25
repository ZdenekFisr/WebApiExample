namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to insert a row into DB directly from an entity instance.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface ICreateEntity<T> where T : Entity
    {
        /// <summary>
        /// Inserts a row into DB directly from an entity instance.
        /// </summary>
        /// <param name="entity">Instance of entity.</param>
        /// <returns></returns>
        Task CreateAsync(T entity);
    }
}
