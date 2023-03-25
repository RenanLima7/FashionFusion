using WebLuto.Common;
using WebLuto.Models.Enums.ProductEnum;

namespace WebLuto.Models
{
    public class Payment : BaseEntity
    {
        public PaymentType Type { get; set; }

        public decimal TotalValue { get; set; }

        public int TotalInstallment { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
