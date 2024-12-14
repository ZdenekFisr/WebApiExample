using Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Insert
{
    ///<summary>
    /// Interface for inserting a row into the database.
    /// </summary>
    /// <typeparam name="TDto">The type of the data transfer object.</typeparam>
    public interface IInsertOperation<TDto>
        where TDto : ModelBase
    {
        /// <summary>
        /// Asynchronously inserts a row into the database.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="dto">The data transfer object containing the data to insert.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InsertAsync(DbContext dbContext, TDto dto, string userId);
    }
}
