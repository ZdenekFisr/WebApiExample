using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing rail vehicles.
    /// </summary>
    public interface IRailVehicleListRepository<TModel> : IGetManyWithUser<TModel>
        where TModel : ModelBase
    {
    }
}
