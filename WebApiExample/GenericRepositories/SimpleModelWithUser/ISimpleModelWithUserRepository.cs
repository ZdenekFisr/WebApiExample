using WebApiExample.RepositoryInterfaces;

namespace WebApiExample.GenericRepositories.SimpleModelWithUser
{
    /// <summary>
    /// Contains CRUD operations for models mapped from database entities that have no foreign key to other database tables and are bound to users with user ID. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TModel">Type of corresponding model that is mapped to the entity with AutoMapper and vice-versa.</typeparam>
    public interface ISimpleModelWithUserRepository<TModel> : IGetOneWithUser<TModel>, ICreateWithUser<TModel>, IUpdateWithUser<TModel>, IDeleteWithUser
        where TModel : Model
    {
    }
}
