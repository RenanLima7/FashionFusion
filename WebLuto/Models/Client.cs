using System.ComponentModel.DataAnnotations.Schema;

namespace WebLuto.Models
{
    public class Client : BaseEntity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Salt { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public string? Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Avatar { get; set; }
    }
}