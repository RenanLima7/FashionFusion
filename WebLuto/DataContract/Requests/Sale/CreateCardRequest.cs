using System.ComponentModel.DataAnnotations;

namespace WebLuto.DataContract.Requests
{
    public sealed class CreateCardRequest
    {
        [Required]
        public string Number { get; set; }

        ///<example>123</example>
        [Required]
        public string CVV { get; set; }

        ///<example>08-31</example>
        [Required]
        public string ExpirationDate { get; set; }

        ///<example>Francisco Renan Lima Rodrigues</example>
        [Required]
        public string FullName { get; set; }
    }
}
