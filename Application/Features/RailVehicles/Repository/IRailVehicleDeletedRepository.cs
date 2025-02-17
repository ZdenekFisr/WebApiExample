using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing deleted rail vehicles.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public interface IRailVehicleDeletedRepository<TModel> : IGetManyWithUser<TModel>, IRestoreWithUser, IHardDeleteWithUser
        where TModel : ModelBase
    {
    }
}
