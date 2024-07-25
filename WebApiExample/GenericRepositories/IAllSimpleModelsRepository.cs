using WebApiExample.RepositoryInterfaces;

namespace WebApiExample.GenericRepositories
{
    /// <summary>
    /// Extension of <see cref="SimpleModelRepository{TEntity, TModel}"/> with the method to return all models present in the database table.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TModel">Type of corresponding model that is mapped to the entity with AutoMapper and vice-versa</typeparam>
    public interface IAllSimpleModelsRepository<TModel> : ISimpleModelRepository<TModel>, IGetAllModels<TModel>
        where TModel : Model
    {
    }
}
