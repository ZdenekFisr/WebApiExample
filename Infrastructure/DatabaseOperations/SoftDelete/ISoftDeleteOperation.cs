using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.SoftDelete
{
    /// <summary>
    /// Interface for performing soft delete operations on entities.
    /// </summary>
    public interface ISoftDeleteOperation
    {
        /// <summary>
        /// Soft deletes an entity by its identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be soft deleted.</typeparam>
        /// <param name="dbContext">The database context to be used for the soft delete operation.</param>
        /// <param name="id">The unique identifier of the entity to be soft deleted.</param>
        /// <param name="userId">The identifier of the user performing the soft delete operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SoftDeleteAsync<TEntity>(DbContext dbContext, Guid id, string userId)
            where TEntity : EntityWithUserBase;
    }
}
