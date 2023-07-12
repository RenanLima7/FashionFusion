namespace WebLuto.DataContract.Requests
{
    public sealed class CreateSaleRequest
    {
        public ICollection<CreateProductRequest> Products { get; set; }

        public CreateCardRequest Card { get; set; }

        public decimal TotalValue { get; set; }
    }
}
