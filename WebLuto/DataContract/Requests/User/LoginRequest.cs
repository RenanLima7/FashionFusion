using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebLuto.DataContract.Requests
{
    public sealed class LoginRequest
    {
        [Required, MaxLength(250), EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8), PasswordPropertyText]
        public string Password { get; set; }
    }
}
