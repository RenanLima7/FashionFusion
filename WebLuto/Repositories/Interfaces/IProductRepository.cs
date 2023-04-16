using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> GetProductById(long id);

        Task<Product> CreateProduct(Product productToCreate);

        Task<Product> UpdateProduct(Product productToUpdate, Product existingProduct);

        Task<bool> DeleteProduct(Product product);
    }
}
