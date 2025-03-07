namespace Domain.Enums
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        PayingUser = 1,
        CompanyUser = 2,
        CompanyAdmin = 4,
        Admin = 8
    }
}
