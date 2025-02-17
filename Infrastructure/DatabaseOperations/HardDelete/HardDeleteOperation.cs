using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.HardDelete
{
    /// <summary>
    /// Service for performing hard delete operations on entities.
    /// </summary>
    public class HardDeleteOperation : IHardDeleteOperation
    {
        /// <inheritdoc />
        public async Task HardDeleteAsync<TEntity>(DbContext dbContext, Guid id, string userId)
            where TEntity : EntityWithUserBase
        {
            TEntity? entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity is null || entity.UserId != userId)
                return;

            dbContext.Set<TEntity>().Remove(entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
