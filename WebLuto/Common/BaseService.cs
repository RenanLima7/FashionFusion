using WebLuto.Common.Interfaces;

namespace WebLuto.Common.Service
{
    public class BaseService : IBaseService
    {
        private readonly IBaseRepository _baseRepository;

        public BaseService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseEntity
        {
            try
            {
                return await _baseRepository.GetAllAsync<T>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetByIdAsync<T>(long id) where T : BaseEntity
        {
            try
            {
                return await _baseRepository.GetByIdAsync<T>(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Create<T>(T entity) where T : BaseEntity
        {
            try
            {
                return await _baseRepository.Create(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Update<T, E>(T entity, E newEntity) where T : BaseEntity where E : BaseEntity
        {
            try
            {
                return await _baseRepository.Update(entity, newEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete<T>(T entity) where T : BaseEntity
        {
            try
            {
                return await _baseRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
