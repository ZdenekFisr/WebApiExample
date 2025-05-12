using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Insert
{
    ///<summary>
    /// Interface for performing insert operations on entities.
    /// </summary>
    public interface IInsertOperation
    {
        /// <summary>
        /// Asynchronously inserts a row into the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be inserted.</typeparam>
        /// <param name="dbContext">The database context to be used for the insert operation.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        /// <param name="mappingMethod">The method used to map the entity.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InsertAsync<TEntity>(DbContext dbContext, string userId, Func<TEntity> mappingMethod)
            where TEntity : EntityWithUserBase;
    }
}
