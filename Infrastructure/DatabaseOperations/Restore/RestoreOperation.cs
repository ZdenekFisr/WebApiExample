using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Restore
{
    /// <summary>
    /// Service for performing restore operations on entities.
    /// </summary>
    public class RestoreOperation : IRestoreOperation
    {
        /// <inheritdoc />
        public async Task RestoreAsync<TEntity>(DbContext dbContext, Guid id, string userId)
            where TEntity : EntityWithUserBase
        {
            TEntity? entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity is null || entity.UserId != userId || entity is not ISoftDeletable softDeletableEntity)
                return;

            softDeletableEntity.IsDeleted = false;

            await dbContext.SaveChangesAsync();
        }
    }
}
