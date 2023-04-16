using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> GetProductById(long id);

        Task<Product> CreateProduct(Product productToCreate);

        Task<Product> UpdateProduct(Product productToUpdate, Product existingProduct);

        Task<bool> DeleteProduct(Product productToDelete);
    }
}