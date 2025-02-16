using Application.Services;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.SoftDelete
{
    /// <summary>
    /// Service for performing soft delete operations on entities.
    /// </summary>
    /// <param name="currentUtcTimeProvider">The provider for the current UTC time.</param>
    public class SoftDeleteOperation(
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : ISoftDeleteOperation
    {
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task SoftDeleteAsync<TEntity>(DbContext dbContext, Guid id, string userId)
            where TEntity : EntityWithUserBase
        {
            TEntity? entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity is null || entity.UserId != userId || entity is not ISoftDeletable softDeletableEntity)
                return;

            softDeletableEntity.IsDeleted = true;
            softDeletableEntity.DeletedAt = _currentUtcTimeProvider.GetCurrentUtcTime();
            softDeletableEntity.DeletedBy = userId;

            await dbContext.SaveChangesAsync();
        }
    }
}
