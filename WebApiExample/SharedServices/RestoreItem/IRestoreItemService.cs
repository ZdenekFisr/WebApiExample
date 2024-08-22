namespace WebApiExample.SharedServices.RestoreItem
{
    /// <summary>
    /// Contains a method for restoring a soft-deleted item.
    /// </summary>
    public interface IRestoreItemService<TEntity>
        where TEntity : EntityWithUser
    {
        /// <summary>
        /// Finds an item in a DB table by ID and if it is soft-deletable, restores it.
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <param name="userId">ID of the user.</param>
        /// <returns></returns>
        Task RestoreAsync(Guid id, string userId);
    }
}
