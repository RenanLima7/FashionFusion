using WebLuto.Models.AbstractModel;

namespace WebLuto.Models
{
    public class Address
    {
        public long Id { get; set; }

        public Person Person { get; set; }

        public string AddressLine { get; set; }

        public string? Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string? ReferencePoint { get; set; }

        public string? ZipCode { get; set; }
    }
}
