using Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Insert
{
    ///<summary>
    /// Interface for inserting a row into the database.
    /// </summary>
    /// <typeparam name="TModel">The type of the model, which must inherit from <see cref="ModelBase"/>.</typeparam>
    public interface IInsertOperation<TModel>
        where TModel : ModelBase
    {
        /// <summary>
        /// Asynchronously inserts a row into the database.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="model">The model containing the data to insert.</param>
        /// <param name="userId">The ID of the user performing the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InsertAsync(DbContext dbContext, TModel model, string userId);
    }
}
