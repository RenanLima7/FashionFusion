using WebLuto.Models.Enums.ProductEnum;

namespace WebLuto.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public bool Quantity { get; set; }

        public string? Dimension { get; set; }

        public string? Image { get; set; }
    }
}
