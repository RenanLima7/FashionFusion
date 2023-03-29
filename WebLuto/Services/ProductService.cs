using WebLuto.DAO;
using WebLuto.Models;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class ProductService : IProductService
    {

        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
