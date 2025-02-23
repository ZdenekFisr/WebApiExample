using Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.Model
{
    public class UserRegisterModel : ModelBase
    {
        [StringLength(30)]
        public required string UserName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
