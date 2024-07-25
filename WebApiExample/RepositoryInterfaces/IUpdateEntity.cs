namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to update a row in DB directly from an entity instance.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IUpdateEntity<T> where T : Entity
    {
        /// <summary>
        /// Updates a row in DB directly from an entity instance.
        /// </summary>
        /// <param name="entity">Instance of entity with the new data.</param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
    }
}
