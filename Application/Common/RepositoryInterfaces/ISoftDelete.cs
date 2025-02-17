namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to soft delete a row from DB.
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Soft deletes a row with a specified ID from DB.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns></returns>
        Task SoftDeleteAsync(Guid id);
    }
}
