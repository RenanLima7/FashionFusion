using System.ComponentModel.DataAnnotations;

namespace WebLuto.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
