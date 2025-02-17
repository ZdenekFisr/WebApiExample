using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.HardDelete
{
    /// <summary>
    /// Interface for performing hard delete operations on entities.
    /// </summary>
    public interface IHardDeleteOperation
    {
        /// <summary>
        /// Hard deletes an entity by its identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be hard deleted.</typeparam>
        /// <param name="dbContext">The database context to be used for the hard delete operation.</param>
        /// <param name="id">The unique identifier of the entity to be hard deleted.</param>
        /// <param name="userId">The identifier of the user performing the hard delete operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task HardDeleteAsync<TEntity>(DbContext dbContext, Guid id, string userId)
            where TEntity : EntityWithUserBase;
    }
}