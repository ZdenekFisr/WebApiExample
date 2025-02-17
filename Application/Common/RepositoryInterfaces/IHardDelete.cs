namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to hard delete a row from DB.
    /// </summary>
    public interface IHardDelete
    {
        /// <summary>
        /// Hard deletes a row with a specified ID from DB.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns></returns>
        Task HardDeleteAsync(Guid id);
    }
}
