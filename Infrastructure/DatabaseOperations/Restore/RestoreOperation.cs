using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Restore
{
    /// <summary>
    /// Represents an operation to restore a soft-deleted entity in the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to be restored. Must inherit from UserEntityBase.</typeparam>
    public class RestoreOperation<TEntity> : IRestoreOperation
            where TEntity : EntityWithUserBase
    {
        /// <inheritdoc />
        public async Task RestoreAsync(DbContext dbContext, Guid id, string userId)
        {
            TEntity? entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity is null || entity.UserId != userId || entity is not ISoftDeletable softDeletableEntity)
                return;

            softDeletableEntity.IsDeleted = false;

            await dbContext.SaveChangesAsync();
        }
    }
}
