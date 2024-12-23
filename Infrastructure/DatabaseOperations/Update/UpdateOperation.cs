﻿using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Update
{
    /// <summary>
    /// Represents an operation to update an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class UpdateOperation<TEntity, TModel>(
        IMapper mapper,
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IUpdateOperation<TEntity, TModel>
        where TEntity : EntityWithUserBase
        where TModel : ModelBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task UpdateAsync(DbContext dbContext, Func<Guid, string, Task<TEntity?>> findEntityMethod, Guid id, TModel newModel, string userId)
        {
            TEntity? existingEntity = await findEntityMethod(id, userId);
            if (existingEntity is null)
                return;

            _mapper.Map(newModel, existingEntity);

            if (existingEntity is IUpdateHistory entityUpdateHistory)
            {
                entityUpdateHistory.UpdatedAt = _currentUtcTimeProvider.GetCurrentUtcTime();
                entityUpdateHistory.UpdatedBy = userId;
            }

            dbContext.Update(existingEntity);

            await dbContext.SaveChangesAsync();
        }
    }
}
