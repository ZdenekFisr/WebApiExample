namespace Application.Features.Auth.Model
{
    public class RefreshTokenRequestModel
    {

        public required string UserId { get; set; }

        public required string RefreshToken { get; set; }
    }
}
