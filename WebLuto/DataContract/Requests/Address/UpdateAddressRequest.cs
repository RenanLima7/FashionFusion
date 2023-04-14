namespace WebLuto.DataContract.Requests
{
    public class UpdateAddressRequest
    {
        public string? ZipCode { get; set; }

        public string? AddressLine { get; set; }

        public string? AddressLineNumber { get; set; }

        public string? Neighborhood { get; set; }
    }
}
