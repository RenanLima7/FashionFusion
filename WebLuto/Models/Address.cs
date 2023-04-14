namespace WebLuto.Models
{
    public class Address : BaseEntity
    {
        public string ZipCode { get; set; }

        public string AddressLine { get; set; }

        public string AddressLineNumber { get; set; }

        public string Neighborhood { get; set; }
    }
}
