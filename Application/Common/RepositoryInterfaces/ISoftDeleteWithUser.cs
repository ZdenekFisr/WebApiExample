namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Interface for performing soft delete operations on entities.
    /// </summary>
    public interface ISoftDeleteWithUser
    {
        /// <summary>
        /// Soft deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be soft deleted.</param>
        /// <param name="userId">The identifier of the user performing the soft delete operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SoftDeleteAsync(Guid id, string userId);
    }
}
