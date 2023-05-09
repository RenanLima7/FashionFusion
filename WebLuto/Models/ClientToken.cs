using System.ComponentModel.DataAnnotations.Schema;
using WebLuto.Common;

namespace WebLuto.Models
{
    public class ClientToken : BaseEntity
    {
        [ForeignKey("Client")]
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string Token { get; set; }
    }
}
