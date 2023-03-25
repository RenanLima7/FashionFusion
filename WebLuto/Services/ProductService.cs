using WebLuto.DAO;
using WebLuto.Models;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class ProductService : IProductService
    {

        private readonly ProductDAO _productDAO;

        public ProductService(ProductDAO productDAO)
        {
            _productDAO = productDAO;
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
