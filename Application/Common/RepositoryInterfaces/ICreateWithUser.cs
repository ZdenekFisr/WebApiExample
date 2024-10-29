namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to insert a row into DB from a model instance and the user ID.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface ICreateWithUser<T> where T : Model
    {
        /// <summary>
        /// Inserts a row into DB from a model instance that is mapped to an entity instance and the user ID.
        /// </summary>
        /// <param name="model">Instance of model.</param>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task CreateAsync(T model, string userId);
    }
}
