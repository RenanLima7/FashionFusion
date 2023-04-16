using System.ComponentModel.DataAnnotations;

namespace WebLuto.DataContract.Requests
{
    public class CreateAddressRequest
    {
        [Required, MaxLength(10)]
        public string ZipCode { get; set; }

        [Required, MaxLength(250)]
        public string AddressLine { get; set; }

        [Required, MaxLength(7)]
        public string AddressLineNumber { get; set; }

        [Required, MaxLength(100)]
        public string Neighborhood { get; set; }
    }
}
