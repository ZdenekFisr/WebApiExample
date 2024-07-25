namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to insert a row into DB from a model instance.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface ICreateModel<T> where T : Model
    {
        /// <summary>
        /// Inserts a row into DB from a model instance that is mapped to an entity instance.
        /// </summary>
        /// <param name="model">Instance of model.</param>
        /// <returns></returns>
        Task CreateAsync(T model);
    }
}
