using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Responses
{
    public sealed class UpdateProductResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Dimension { get; set; }

        public string? Image { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
