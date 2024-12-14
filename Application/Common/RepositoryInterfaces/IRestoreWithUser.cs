namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Represents a restore operation for soft-deletable entities.
    /// </summary>
    public interface IRestoreWithUser
    {
        /// <summary>
        /// Sets 'IsDeleted' of a row with a specified ID to 0. For better security, user ID is also required.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task RestoreAsync(Guid id, string userId);
    }
}
