using WebLuto.Models.Enums.ProductEnum;

namespace WebLuto.Models
{
    public class Sale //: BaseEntity
    {
        public long id { get; set; }

        public List<Product> ProductList { get; set; }

        public Client Client { get; set; }

        public Payment Payment { get; set; }

        public decimal TotalValue { get; set; }

        public int? TotalInstallment { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
