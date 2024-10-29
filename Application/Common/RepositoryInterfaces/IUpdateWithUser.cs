namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to update a row in DB from a model instance, the ID and the user ID.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IUpdateWithUser<T> where T : Model
    {
        /// <summary>
        /// Updates a row in DB from a model instance, the ID and the user ID.
        /// </summary>
        /// <param name="id">ID of the row.</param>
        /// <param name="model">Instance of model with the new data.</param>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, T model, string userId);
    }
}
