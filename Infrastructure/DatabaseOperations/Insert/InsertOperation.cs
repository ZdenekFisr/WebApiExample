using Application.Services;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Insert
{
    /// <summary>
    /// Service for performing insert operations on entities.
    /// </summary>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class InsertOperation(
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IInsertOperation
    {
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task InsertAsync<TEntity>(DbContext dbContext, string userId, Func<TEntity> mappingMethod)
            where TEntity : EntityWithUserBase
        {
            TEntity entity = mappingMethod();
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
