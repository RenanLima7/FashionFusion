using FashionFusion.Models;

namespace FashionFusion.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(Product product);

        Task<Product> GetProductById(long id);
    }
}