using System.ComponentModel.DataAnnotations.Schema;
using WebLuto.Common;

namespace WebLuto.Models
{
    public class Address : BaseEntity
    {
        [ForeignKey("Client")]
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string ZipCode { get; set; }

        public string AddressLine { get; set; }

        public string AddressLineNumber { get; set; }

        public string Neighborhood { get; set; }
    }
}
