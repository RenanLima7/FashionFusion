using WebLuto.Models.Enums;

namespace WebLuto.Models
{
    public class Payment //: BaseEntity
    {
        public long Id { get; set; }

        public Client Client { get; set; }

        public PaymentType Type { get; set; }

        public string PaymentHashCode { get; set; }
    }
}
