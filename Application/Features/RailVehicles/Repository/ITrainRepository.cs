using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for handling one train.
    /// </summary>
    /// <typeparam name="TInputModel">Type of input model.</typeparam>
    /// <typeparam name="TOutputModel">Type of output model.</typeparam>
    public interface ITrainRepository<TInputModel, TOutputModel> : IGetOneWithUser<TOutputModel>, ICreateWithUser<TInputModel>, IUpdateWithUser<TInputModel>
        where TInputModel : ModelBase
        where TOutputModel : ModelBase
    {
    }
}
