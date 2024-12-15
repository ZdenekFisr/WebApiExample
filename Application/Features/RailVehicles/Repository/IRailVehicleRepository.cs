using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for handling one rail vehicle.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public interface IRailVehicleRepository<TModel> : IGetOneWithUser<TModel>, ICreateWithUser<TModel>, IUpdateWithUser<TModel>
        where TModel : ModelBase
    {
    }
}
