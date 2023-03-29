using WebLuto.Models.Enums.ProductEnum;

namespace WebLuto.Models
{
    public class Payment
    {
        public long Id { get; set; }

        public Client Client { get; set; }

        public PaymentType Type { get; set; }

        public string PaymentHashCode { get; set; }
    }
}
