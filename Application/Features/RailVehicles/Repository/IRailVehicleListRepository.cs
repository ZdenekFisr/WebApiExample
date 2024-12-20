﻿using Application.Common;
using Application.Common.RepositoryInterfaces;

namespace Application.Features.RailVehicles.Repository
{
    /// <summary>
    /// Repository for listing rail vehicles.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public interface IRailVehicleListRepository<TModel> : IGetManyWithUser<TModel>, ISoftDeleteWithUser
        where TModel : ModelBase
    {
    }
}
