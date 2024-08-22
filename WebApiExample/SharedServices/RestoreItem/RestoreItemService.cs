using Microsoft.EntityFrameworkCore;
using WebApiExample.EntityInterfaces;

namespace WebApiExample.SharedServices.RestoreItem
{
    /// <summary>
    /// Contains a method for restoring a soft-deleted item.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public class RestoreItemService<TEntity> : IRestoreItemService<TEntity>
        where TEntity : EntityWithUser
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<TEntity> _entities;

        public RestoreItemService(ApplicationDbContext dbContext)
        {
            _context = dbContext;

            _entities = _context.Set<TEntity>();
        }

        /// <inheritdoc cref="IRestoreItemService{TEntity}.RestoreAsync(Guid, string)"/>
        public async Task RestoreAsync(Guid id, string userId)
        {
            TEntity? entity = await _entities.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (entity is null)
                return;

            if (entity is not ISoftDeletable softDeletableEntity)
                return;

            softDeletableEntity.IsDeleted = false;
            await _context.SaveChangesAsync();
        }
    }
}
