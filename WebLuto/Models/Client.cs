namespace WebLuto.Models
{
    public class Client
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public Address Address { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}
