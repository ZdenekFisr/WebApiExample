using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.GenericRepositories
{
    /// <summary>
    /// Contains CRUD operations for input and output models mapped from database entities that have no foreign key to other database tables and are bound to users with user ID. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TInputModel">Model used to create or update a DB item.</typeparam>
    /// <typeparam name="TOutputModel">Model used to get a DB item.</typeparam>
    public interface ISimpleModelWithUserRepository<TInputModel, TOutputModel> : IGetOneWithUser<TOutputModel>, ICreateWithUser<TInputModel>, IUpdateWithUser<TInputModel>, ISoftDeleteWithUser
        where TInputModel : ModelBase
        where TOutputModel : ModelBase
    {
    }

    /// <summary>
    /// Contains CRUD operations for two-way models mapped from database entities that have no foreign key to other database tables and are bound to users with user ID. It can be inherited by other services and its methods can be overridden to suit a more complex model.
    /// </summary>
    /// <typeparam name="TModel">Model used to create, update or get a DB item.</typeparam>
    public interface ISimpleModelWithUserRepository<TModel> : ISimpleModelWithUserRepository<TModel, TModel>
        where TModel : ModelBase
    {
    }
}
