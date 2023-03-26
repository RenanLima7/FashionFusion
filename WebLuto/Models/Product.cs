using WebLuto.Common;
using WebLuto.Models.Enums.ProductEnum;

namespace WebLuto.Models
{
    public class Product : BaseEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public bool? Solt { get; set; }
    }
}
