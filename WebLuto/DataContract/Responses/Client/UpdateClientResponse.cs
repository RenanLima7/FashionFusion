using WebLuto.Models;

namespace WebLuto.DataContract.Responses
{
    public class UpdateClientResponse
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public Address Address { get; set; }

        public string? Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Avatar { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
