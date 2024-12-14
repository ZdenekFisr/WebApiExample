using Application.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Update
{
    /// <summary>
    /// Defines the contract for updating an entity with the provided model.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to be updated, which must inherit from <see cref="EntityWithUserBase"/>.</typeparam>
    /// <typeparam name="TModel">The type of the model, which must inherit from <see cref="ModelBase"/>.</typeparam>
    public interface IUpdateOperation<TEntity, TModel>
        where TEntity : EntityWithUserBase
        where TModel : ModelBase
    {

        /// <summary>
        /// Updates an entity asynchronously with the provided model.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="findEntityMethod">A method to find the entity based on the provided ID and user ID.</param>
        /// <param name="id">The unique identifier of the entity to be updated.</param>
        /// <param name="newModel">The model containing the new values for the entity.</param>
        /// <param name="userId">The unique identifier of the user performing the update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAsync(DbContext dbContext, Func<Guid, string, Task<TEntity?>> findEntityMethod, Guid id, TModel newModel, string userId);
    }
}
