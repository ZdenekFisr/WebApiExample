using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for handling electrification types.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    /// <typeparam name="TListModel">Type of model for list.</typeparam>
    public interface IElectrificationTypeRepository<TModel, TListModel> : IGetManyWithUser<TListModel>, ICreateWithUser<TModel>, IUpdateWithUser<TModel>, IHardDeleteWithUser
        where TModel : ModelBase
        where TListModel : ModelBase
    {
    }
}
