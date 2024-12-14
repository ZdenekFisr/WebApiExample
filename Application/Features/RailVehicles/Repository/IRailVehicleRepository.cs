using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    public interface IRailVehicleRepository<TModel> : IGetOneWithUser<TModel>, ICreateWithUser<TModel>, IUpdateWithUser<TModel>, ISoftDeleteWithUser, IRestoreWithUser
        where TModel : ModelBase
    {
    }
}
