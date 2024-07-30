using Microsoft.EntityFrameworkCore;
using WebApiExample.Extensions;

namespace WebApiExample.GeneralServices.RestoreItem
{
    /// <summary>
    /// Contains a method for restoring a soft-deleted item.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public class RestoreItemService<TEntity> : IRestoreItemService
        where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<TEntity> _entities;

        public RestoreItemService(ApplicationDbContext dbContext)
        {
            _context = dbContext;

            _entities = _context.Set<TEntity>();
        }

        /// <inheritdoc cref="IRestoreItemService.Restore(Guid)"/>
        public async Task Restore(Guid id)
        {
            var item = await _entities.FirstOrDefaultAsync(rv => rv.Id == id);

            if (item is null)
                return;

            item.Restore();
            await _context.SaveChangesAsync();
        }
    }
}
