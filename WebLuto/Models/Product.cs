using WebLuto.Common;

namespace WebLuto.Models
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Dimension { get; set; }

        public string? Image { get; set; }
    }
}
