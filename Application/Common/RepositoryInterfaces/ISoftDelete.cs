﻿namespace Application.Common.RepositoryInterfaces
{
    /// <summary>
    /// Contains a method to delete a row from DB.
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Deletes a row with a specified ID from DB.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
