namespace WebApiExample.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to delete a row from DB. For better security, user ID is also required.
    /// </summary>
    public interface IDeleteWithUser
    {
        /// <summary>
        /// Deletes a row with a specified ID from DB. For better security, user ID is also required.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, string userId);
    }
}
