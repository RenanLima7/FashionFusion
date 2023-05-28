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

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
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

        public async Task<T> GetByIdAsync<T>(long id) where T : class
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

        public async Task<T> Create<T>(T entity) where T : class
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

        public async Task<T> Update<T>(T entity) where T : class
        {
            try
            {
                return await _baseRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete<T>(T entity) where T : class
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
