namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to update a row in DB from a model instance and the ID.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IUpdateModel<T> where T : Model
    {
        /// <summary>
        /// Updates a row in DB from a model instance and the ID.
        /// </summary>
        /// <param name="id">ID of the row.</param>
        /// <param name="model">Instance of model with the new data.</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, T model);
    }
}
