using Microsoft.EntityFrameworkCore;
using WebLuto.Common.Interfaces;
using WebLuto.DataContext;

namespace WebLuto.Common.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly WLContext _dbContext;

        public BaseRepository(WLContext wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tudo - {ex.Message}");
            }
        }

        public async Task<T> GetByIdAsync<T>(long id) where T : class
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o id {id} - {ex.Message}");
            }
        }

        public async Task<T> Create<T>(T entity) where T : class
        {
            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar - {ex.Message}");
            }
        }

        public async Task<T> Update<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar - {ex.Message}");
            }
        }

        public async Task<bool> Delete<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar - {ex.Message}");
            }
        }
    }
}
