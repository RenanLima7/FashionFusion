using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                List<Product> productList = await _productRepository.GetAllProducts();

                return productList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductById(long id)
        {
            try
            {
                Product product = await _productRepository.GetProductById(id);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> CreateProduct(Product productToCreate)
        {
            try
            {
                Product productCreated = await _productRepository.CreateProduct(productToCreate);

                return productCreated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> UpdateProduct(Product productToUpdate, Product existingProduct)
        {
            try
            {
                Product productUpdated = await _productRepository.UpdateProduct(productToUpdate, existingProduct);

                return productUpdated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(Product productToDelete)
        {
            try
            {
                return await _productRepository.DeleteProduct(productToDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
