using WebLuto.Common;

namespace WebLuto.Models
{
    public class Card : BaseEntity
    {
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string Number { get; set; }

        ///<example>123</example>
        public string CVV { get; set; }

        ///<example>08-31</example>
        public string ExpirationDate { get; set; }

        ///<example>Francisco Renan Lima Rodrigues</example>
        public string FullName { get; set; }
    }
}
