using Application.Services;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.SoftDelete
{
    /// <summary>
    /// Represents an operation to perform a soft delete on an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="currentUtcTimeProvider">The provider for the current UTC time.</param>
    public class SoftDeleteOperation<TEntity>(
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : ISoftDeleteOperation
            where TEntity : EntityWithUserBase
    {
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task SoftDeleteAsync(DbContext dbContext, Guid id, string userId)
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
