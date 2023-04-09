using Microsoft.EntityFrameworkCore;
using WebLuto.Data;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;

namespace WebLuto.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WLDBContext _dbContext;

        public ProductRepository(WLDBContext wLDBContext)
        {
            _dbContext = wLDBContext;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _dbContext.Product
                    .Where(x => x.DeletionDate == null)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao buscar todos os produtos!"));
            }
        }

        public async Task<Product> GetProductById(long id)
        {
            try
            {
                return await _dbContext.Product.FirstOrDefaultAsync
                (
                    x => x.Id == id &&
                    x.DeletionDate == null
                );
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao buscar um produto com o Id: {0}", id));
            }
        }

        public async Task<Product> CreateProduct(Product productToCreate)
        {
            try
            {
                productToCreate.Name = productToCreate.Name;
                productToCreate.Price = productToCreate.Price;
                productToCreate.Type = productToCreate.Type;
                productToCreate.Quantity = productToCreate.Quantity;
                productToCreate.Dimension = productToCreate.Dimension;
                productToCreate.Image = productToCreate.Image;
                productToCreate.CreationDate = DateTime.Now;

                await _dbContext.Product.AddAsync(productToCreate);
                await _dbContext.SaveChangesAsync();

                return productToCreate;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao criar o produto!"));
            }
        }

        public async Task<Product> UpdateProduct(Product productToUpdate, Product existingProduct)
        {
            try
            {
                existingProduct.Name = productToUpdate.Name ?? existingProduct.Name;
                existingProduct.Price = productToUpdate.Price != existingProduct.Price ? productToUpdate.Price : existingProduct.Price;
                existingProduct.Type = productToUpdate.Type != existingProduct.Type ? productToUpdate.Type : existingProduct.Type;
                existingProduct.Quantity = productToUpdate.Quantity != existingProduct.Quantity ? productToUpdate.Quantity : existingProduct.Quantity;
                existingProduct.Dimension = productToUpdate.Dimension ?? existingProduct.Dimension;
                existingProduct.Image = productToUpdate.Image ?? existingProduct.Image;
                existingProduct.UpdateDate = DateTime.Now;

                _dbContext.Product.Update(existingProduct);
                await _dbContext.SaveChangesAsync();

                return existingProduct;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao atualizar o produto: {0}", existingProduct.Id));
            }
        }

        public async Task<bool> DeleteProduct(Product productToDelete)
        {
            try
            {
                //_dbContext.Product.Remove(productToDelete);

                productToDelete.DeletionDate = DateTime.Now;

                _dbContext.Product.Update(productToDelete);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao deletar o produto: {0}", productToDelete.Id));
            }
        }
    }
}
