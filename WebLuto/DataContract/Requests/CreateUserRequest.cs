using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebLuto.Models.Enums.UserEnum;

namespace WebLuto.DataContract.Requests
{
    public sealed class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required, MinLength(8), PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        public UserType Type { get; set; }
    }
}
