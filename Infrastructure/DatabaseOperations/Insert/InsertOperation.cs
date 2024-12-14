using Application.Common;
using Application.Services;
using AutoMapper;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Insert
{
    /// <summary>
    /// Represents an operation to insert an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="mapper">The mapper to map between model and entity.</param>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class InsertOperation<TEntity, TModel>(
        IMapper mapper,
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IInsertOperation<TModel>
            where TEntity : EntityWithUserBase
            where TModel : ModelBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task InsertAsync(DbContext dbContext, TModel model, string userId)
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
