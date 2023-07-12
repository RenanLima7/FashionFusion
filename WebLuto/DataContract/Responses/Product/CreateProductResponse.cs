using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Responses
{
    public sealed class CreateProductResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
