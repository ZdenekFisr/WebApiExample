using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Restore
{
    /// <summary>
    /// Defines the contract for a restore operation.
    /// </summary>
    public interface IRestoreOperation
    {
        /// <summary>
        /// Restores the database to a previous state identified by the specified ID.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="id">The unique identifier of the restore point.</param>
        /// <param name="userId">The identifier of the user performing the restore operation.</param>
        /// <returns>A task that represents the asynchronous restore operation.</returns>
        Task RestoreAsync(DbContext dbContext, Guid id, string userId);
    }
}
