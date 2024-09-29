using WebApiExample.RepositoryInterfaces;

namespace WebApiExample.GenericRepositories.SimpleModel
{
    /// <summary>
    /// Contains CRUD operations for input and output models mapped from database entities that have no foreign key to other database tables. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TInputModel">Model used to create or update a DB item.</typeparam>
    /// <typeparam name="TOutputModel">Model used to get a DB item.</typeparam>
    public interface ISimpleModelRepository<TInputModel, TOutputModel> : IGetOne<TOutputModel>, ICreate<TInputModel>, IUpdate<TInputModel>, IDelete
        where TInputModel : Model
        where TOutputModel : Model
    {
    }

    /// <summary>
    /// Contains CRUD operations for two-way models mapped from database entities that have no foreign key to other database tables. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TModel">Model used to create, update or get a DB item.</typeparam>
    public interface ISimpleModelRepository<TModel> : ISimpleModelRepository<TModel, TModel>
        where TModel : Model
    {
    }
}
