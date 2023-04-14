using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebLuto.Models;

namespace WebLuto.DataContract.Requests
{
    public class UpdateClientRequest
    {
        [MaxLength(250), EmailAddress]
        public string? Email { get; set; }

        [MaxLength(250)]
        public string? Username { get; set; }

        [MinLength(8), PasswordPropertyText]
        public string? Password { get; set; }

        [MaxLength(25)]
        public string? FirstName { get; set; }

        [MaxLength(25)]
        public string? LastName { get; set; }

        [MaxLength(15)]
        public string? CPF { get; set; }

        public Address? Address { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Avatar { get; set; }
    }
}