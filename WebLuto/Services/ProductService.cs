using FashionFusion.DAO;
using FashionFusion.Models;
using FashionFusion.Services.Interfaces;

namespace FashionFusion.Services
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
