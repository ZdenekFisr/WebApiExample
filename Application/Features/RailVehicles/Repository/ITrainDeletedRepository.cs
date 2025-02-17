using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing deleted trains.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface ITrainDeletedRepository<TModel> : IGetManyWithUser<TModel>, IRestoreWithUser, IHardDeleteWithUser
        where TModel : ModelBase
    {
    }
}
