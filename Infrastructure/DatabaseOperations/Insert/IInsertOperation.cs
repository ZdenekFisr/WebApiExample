using Application.Common;
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
        /// <typeparam name="TModel">The type of the data transfer object to be inserted.</typeparam>
        /// <param name="dbContext">The database context to be used for the insert operation.</param>
        /// <param name="dto">The data transfer object containing the data to insert.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InsertAsync<TEntity, TModel>(DbContext dbContext, TModel dto, string userId)
            where TEntity : EntityWithUserBase
            where TModel : ModelBase;
    }
}
