﻿using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Insert
{
    /// <summary>
    /// Service for performing insert operations on entities.
    /// </summary>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class InsertOperation(
        IMapper mapper,
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IInsertOperation
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task InsertAsync<TEntity, TModel>(DbContext dbContext, TModel model, string userId)
            where TEntity : EntityWithUserBase
            where TModel : ModelBase
        {
            TEntity entity = _mapper.Map<TEntity>(model);
            entity.UserId = userId;

            if (entity is ICreateHistory entityCreateHistory)
            {
                entityCreateHistory.CreatedAt = _currentUtcTimeProvider.GetCurrentUtcTime();
                entityCreateHistory.CreatedBy = userId;
            }

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
