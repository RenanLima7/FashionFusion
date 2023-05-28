namespace WebLuto.Common.Interfaces
{
    public interface IBaseService
    {
        public Task<IEnumerable<T>> GetAllAsync<T>() where T : class;

        public Task<T> GetByIdAsync<T>(long id) where T : class;

        public Task<T> Create<T>(T entity) where T : class;

        public Task<T> Update<T>(T entity) where T : class;

        public Task<bool> Delete<T>(T entity) where T : class;
    }
}
