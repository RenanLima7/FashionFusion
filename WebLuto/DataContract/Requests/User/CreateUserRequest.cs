using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Requests
{
    public sealed class CreateUserRequest
    {
        [Required, MaxLength(250)]
        public string Username { get; set; }

        [Required, MinLength(8), PasswordPropertyText]
        public string Password { get; set; }
    }
}
