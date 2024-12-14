namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Represents a restore operation for soft-deletable entities.
    /// </summary>
    public interface IRestore
    {
        /// <summary>
        /// Sets 'IsDeleted' of a row with a specified ID to 0.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns></returns>
        Task RestoreAsync(Guid id);
    }
}
