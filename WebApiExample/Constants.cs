using Domain.Enums;

namespace WebApiExample
{
    public static class Constants
    {
        public static readonly string[] AllPayingRoles =
        [
            UserRole.PayingUser.ToString(),
            UserRole.CompanyUser.ToString(),
            UserRole.CompanyAdmin.ToString(),
            UserRole.Admin.ToString()
        ];
    }
}
