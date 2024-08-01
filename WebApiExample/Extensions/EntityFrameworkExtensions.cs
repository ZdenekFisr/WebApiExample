using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiExample.EntityInterfaces;
using WebApiExample.Helpers;

namespace WebApiExample.Extensions
{
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Gets a row from a DB table by a given condition. If the entity is soft-deletable, a row is returned only if it is active.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entities">Set of entities, see <see cref="DbSet{TEntity}"/>.</param>
        /// <param name="firstOrDefaultCondition">A condition that the row must meet.</param>
        /// <returns>An object that meets the condition and is active; otherwise, null.</returns>
        public static async Task<TEntity?> FindActiveEntityByPredicate<TEntity>(this DbSet<TEntity> entities, Expression<Func<TEntity, bool>> firstOrDefaultCondition)
            where TEntity : Entity
        {
            var result = await entities.FirstOrDefaultAsync(firstOrDefaultCondition);

            if (result is null || (result is ISoftDeletable softDeletableEntity && softDeletableEntity.IsDeleted))
                return null;

            return result;
        }

        /// <summary>
        /// If the entity is soft-deletable and the soft-delete is allowed, the corresponding properties in the DB are set. Otherwise, the corresponding row is removed from the DB. Warning: the context changes are not saved.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entities">Set of entities, see <see cref="DbSet{TEntity}"/>.</param>
        /// <param name="entity">Instance of entity to be deleted.</param>
        /// <param name="includeSoftDelete">True if soft-delete is preferred over hard-delete; otherwise, false.</param>
        /// <param name="userId">User ID. If the entity is not bound to a user, keep default.</param>
        public static void SoftOrHardDelete<TEntity>(this DbSet<TEntity> entities, TEntity entity, bool includeSoftDelete, string? userId = null)
            where TEntity : Entity
        {
            if (includeSoftDelete && entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeletedAt = DateTimeHelpers.GetCurrentDateTimeUtc();
                softDeletableEntity.DeletedBy = userId;
            }
            else
            {
                entities.Remove(entity);
            }
        }

        /// <summary>
        /// Restores a soft-deleted row by setting <see cref="ISoftDeletable.IsDeleted"/> to false. Warning: the context changes are not saved.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Instance of entity to be restored.</param>
        public static void Restore<TEntity>(this TEntity entity)
            where TEntity : Entity
        {
            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = false;
            }
        }

        /// <summary>
        /// If the entity implements <see cref="ICreateHistory"/>, this method sets the properties about the history of creating the item.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Instance of entity to be modified.</param>
        /// <param name="userId">User ID. If the entity is not bound to a user, keep default.</param>
        public static void SetCreateHistory<TEntity>(this TEntity entity, string? userId = null)
            where TEntity : Entity
        {
            if (entity is ICreateHistory entityCreateHistory)
            {
                entityCreateHistory.CreatedAt = DateTimeHelpers.GetCurrentDateTimeUtc();
                entityCreateHistory.CreatedBy = userId;
            }
        }

        /// <summary>
        /// If the entity implements <see cref="IUpdateHistory"/>, this method sets the properties about the history of updating the item.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Instance of entity to be modified.</param>
        /// <param name="userId">User ID. If the entity is not bound to a user, keep default.</param>
        public static void SetUpdateHistory<TEntity>(this TEntity entity, string? userId = null)
            where TEntity : Entity
        {
            if (entity is IUpdateHistory entityUpdateHistory)
            {
                entityUpdateHistory.UpdatedAt = DateTimeHelpers.GetCurrentDateTimeUtc();
                entityUpdateHistory.UpdatedBy = userId;
            }
        }
    }
}
