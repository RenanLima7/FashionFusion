using WebLuto.Common;
using WebLuto.Models.Enums;

namespace WebLuto.Models
{
    public class Sale : BaseEntity
    {
        public string ProductList { get; set; }

        public long ClientId { get; set; }

        public string Card { get; set; }

        public decimal TotalValue { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
