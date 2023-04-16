using WebLuto.Common;
using WebLuto.Models.Enums;

namespace WebLuto.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public int Quantity { get; set; }

        public string? Dimension { get; set; }

        public string? Image { get; set; }
    }
}
