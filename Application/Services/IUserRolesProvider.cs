using Domain.Enums;

namespace Application.Services
{
    /// <summary>
    /// Defines a contract for providing user roles.
    /// </summary>
    public interface IUserRolesProvider
    {
        /// <summary>
        /// Gets the roles associated with the specified user role.
        /// </summary>
        /// <param name="role">User roles enum.</param>
        /// <returns>Collection of user roles as strings.</returns>
        IEnumerable<string> GetUserRoles(UserRole role);
    }
}