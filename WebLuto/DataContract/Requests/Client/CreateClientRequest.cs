using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebLuto.Models;

namespace WebLuto.DataContract.Requests
{
    public class CreateClientRequest
    {
        [Required, MaxLength(250), EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8), PasswordPropertyText]
        public string Password { get; set; }

        [Required, MaxLength(25)]
        public string FirstName { get; set; }

        [Required, MaxLength(25)]
        public string LastName { get; set; }

        [Required, MaxLength(15)]
        public string CPF { get; set; }

        [Required]
        public CreateAddressRequest Address { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Avatar { get; set; }
    }
}