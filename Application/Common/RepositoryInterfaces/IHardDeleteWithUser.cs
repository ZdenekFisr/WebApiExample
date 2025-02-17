namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to hard delete a row in DB. For better security, user ID is also required.
    /// </summary>
    public interface IHardDeleteWithUser
    {
        /// <summary>
        /// Hard deletes an entity by its ID. For better security, user ID is also required.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task HardDeleteAsync(Guid id, string userId);
    }
}
