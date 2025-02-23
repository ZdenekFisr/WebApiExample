namespace Domain.Entities
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class User
    {
        public string Id { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PasswordHash { get; set; } = default!;

        public string? RefreshToken { get; set; }

        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
    }
}
