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

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseEntity
        {
            try
            {
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tudo - {ex.Message}");
            }
        }

        public async Task<T> GetByIdAsync<T>(long id) where T : BaseEntity
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

        public async Task<T> Create<T>(T entity) where T : BaseEntity
        {
            try
            {
                entity.CreationDate = DateTime.Now;

                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar - {ex.Message}");
            }
        }

        public async Task<T> Update<T, E>(T entity, E newEntity) where T : BaseEntity where E : BaseEntity
        {
            try
            {
                newEntity.UpdateDate = DateTime.Now;

                _dbContext.Entry(entity).CurrentValues.SetValues(newEntity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar - {ex.Message}");
            }
        }

        public async Task<bool> Delete<T>(T entity) where T : BaseEntity
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
