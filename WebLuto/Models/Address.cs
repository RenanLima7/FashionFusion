namespace WebLuto.Models
{
    public class Address //: BaseEntity
    {
        public long Id { get; set; }

        public string AddressLine { get; set; }

        public string AddressLineNumber { get; set; }

        public string Neighborhood { get; set; }

        public string? ZipCode { get; set; }
    }
}
