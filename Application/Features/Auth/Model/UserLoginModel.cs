namespace Application.Features.Auth.Model
{
    public class UserLoginModel
    {
        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
