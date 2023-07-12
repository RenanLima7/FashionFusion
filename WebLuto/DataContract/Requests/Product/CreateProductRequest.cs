using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Requests
{
    public sealed class CreateProductRequest
    {
        public int ProductId  { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(15)]
        public string? Dimension { get; set; }

        public string? Image { get; set; }
    }
}
