using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Restore
{
    /// <summary>
    /// Interface for performing restore operations on entities.
    /// </summary>
    public interface IRestoreOperation
    {
        /// <summary>
        /// Restores the database to a previous state identified by the specified ID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be restored.</typeparam>
        /// <param name="dbContext">The database context to be used for the restore operation.</param>
        /// <param name="id">The unique identifier of the restore point.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        /// <returns>A task that represents the asynchronous restore operation.</returns>
        Task RestoreAsync<TEntity>(DbContext dbContext, Guid id, string userId)
            where TEntity : EntityWithUserBase;
    }
}
