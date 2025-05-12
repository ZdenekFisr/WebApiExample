using Application.Common;
using Application.Services;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Update
{
    /// <summary>
    /// Service for performing update operations on entities without foreign key constraints.
    /// </summary>
    /// <param name="currentUtcTimeProvider">The provider to get the current UTC time.</param>
    public class UpdateOperation(
        ICurrentUtcTimeProvider currentUtcTimeProvider)
        : IUpdateOperation
    {
        private readonly ICurrentUtcTimeProvider _currentUtcTimeProvider = currentUtcTimeProvider;

        /// <inheritdoc />
        public async Task UpdateAsync<TEntity, TModel>(DbContext dbContext, Func<Guid, string, Task<TEntity?>> findEntityMethod, Guid id, TModel newModel, string userId)
            where TEntity : EntityWithUserBase
            where TModel : ModelBase
        {
            TEntity? existingEntity = await findEntityMethod(id, userId);
            if (existingEntity is null)
                return;

            MapProperties(newModel, existingEntity);

            if (existingEntity is IUpdateHistory entityUpdateHistory)
            {
                entityUpdateHistory.UpdatedAt = _currentUtcTimeProvider.GetCurrentUtcTime();
                entityUpdateHistory.UpdatedBy = userId;
            }

            dbContext.Update(existingEntity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Maps properties from the source model to the target entity.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The instance of the source model.</param>
        /// <param name="target">The instance of the target entity.</param>
        private void MapProperties<TModel, TEntity>(TModel source, TEntity target)
        {
            var sourceProperties = typeof(TModel).GetProperties();
            var targetProperties = typeof(TEntity).GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                var targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    var value = sourceProperty.GetValue(source);
                    targetProperty.SetValue(target, value);
                }
            }
        }
    }
}
