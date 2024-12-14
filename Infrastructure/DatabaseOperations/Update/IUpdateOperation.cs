using Application.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseOperations.Update
{
    /// <summary>
    /// Defines the contract for updating an entity with the provided data transfer object (DTO).
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to be updated, which must inherit from <see cref="UserEntityBase"/>.</typeparam>
    /// <typeparam name="TDto">The type of the data transfer object, which must inherit from <see cref="DtoBase"/>.</typeparam>
    public interface IUpdateOperation<TEntity, TDto>
        where TEntity : EntityWithUserBase
        where TDto : ModelBase
    {

        /// <summary>
        /// Updates an entity asynchronously with the provided data transfer object (DTO).
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="findEntityMethod">A method to find the entity based on the provided ID and user ID.</param>
        /// <param name="id">The unique identifier of the entity to be updated.</param>
        /// <param name="newDto">The data transfer object containing the new values for the entity.</param>
        /// <param name="userId">The unique identifier of the user performing the update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAsync(DbContext dbContext, Func<Guid, string, Task<TEntity?>> findEntityMethod, Guid id, TDto newDto, string userId);
    }
}
