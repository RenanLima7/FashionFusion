using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebLuto.DataContract.Requests
{
    public sealed class LoginRequest
    {
        [Required, MaxLength(250), EmailAddress]
        ///<example>renan1510@gmail.com</example>
        public string Email { get; set; }

        [Required, MinLength(8), PasswordPropertyText]
        ///<example> 12345678 </example>
        public string Password { get; set; }
    }
}
