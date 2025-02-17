using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing trains.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public interface ITrainListRepository<TModel> : IGetManyWithUser<TModel>, ISoftDeleteWithUser
        where TModel : ModelBase
    {
    }
}
