using Application.Services;
using Domain.Enums;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="IUserRolesProvider"/>
    public class UserRolesProvider : IUserRolesProvider
    {
        /// <inheritdoc />
        public IEnumerable<string> GetUserRoles(UserRole role)
        {
            foreach (UserRole userRole in Enum.GetValues<UserRole>())
            {
                if (role.HasFlag(userRole))
                {
                    yield return userRole.ToString();
                }
            }
        }
    }
}
