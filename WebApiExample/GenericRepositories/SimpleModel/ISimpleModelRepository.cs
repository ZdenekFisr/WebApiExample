using WebApiExample.RepositoryInterfaces;

namespace WebApiExample.GenericRepositories.SimpleModel
{
    /// <summary>
    /// Contains CRUD operations for models mapped from database entities that have no foreign key to other database tables. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TModel">Type of corresponding model that is mapped to the entity with AutoMapper and vice-versa.</typeparam>
    public interface ISimpleModelRepository<TModel> : IGetOne<TModel>, ICreate<TModel>, IUpdate<TModel>, IDelete
        where TModel : Model
    {
    }
}
