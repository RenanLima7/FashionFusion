namespace WebLuto.DataContract.Responses
{
    public class UpdateAddressResponse
    {
        public long Id { get; set; }

        public string ZipCode { get; set; }

        public string AddressLine { get; set; }

        public string AddressLineNumber { get; set; }

        public string Neighborhood { get; set; }
    }
}
