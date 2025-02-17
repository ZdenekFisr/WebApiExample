using Application.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Update
{
    /// <summary>
    /// Interface for performing update operations on entities.
    /// </summary>
    public interface IUpdateOperation
    {
        /// <summary>
        /// Updates an entity asynchronously with the provided model.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be updated.</typeparam>
        /// <typeparam name="TModel">The type of the model to be used for the update operation.</typeparam>
        /// <param name="dbContext">The database context to be used for the update operation.</param>
        /// <param name="findEntityMethod">A method to find the entity based on the provided ID and user ID.</param>
        /// <param name="id">The unique identifier of the entity to be updated.</param>
        /// <param name="newModel">The model containing the new values for the entity.</param>
        /// <param name="userId">The unique identifier of the user performing the update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAsync<TEntity, TModel>(DbContext dbContext, Func<Guid, string, Task<TEntity?>> findEntityMethod, Guid id, TModel newModel, string userId)
            where TEntity : EntityWithUserBase
            where TModel : ModelBase;
    }
}
