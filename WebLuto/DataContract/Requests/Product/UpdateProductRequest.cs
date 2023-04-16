using System.ComponentModel.DataAnnotations;
using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Requests
{
    public sealed class UpdateProductRequest
    {
        [MaxLength(250)]
        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public ProductType? Type { get; set; }

        public int? Quantity { get; set; }

        [MaxLength(15)]
        public string? Dimension { get; set; }

        public string? Image { get; set; }
    }
}

