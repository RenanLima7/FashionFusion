namespace WebLuto.Common.Interfaces
{
    public interface IBaseService
    {
        public Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseEntity;

        public Task<T> GetByIdAsync<T>(long id) where T : BaseEntity;

        public Task<T> Create<T>(T entity) where T : BaseEntity;

        public Task<T> Update<T, E>(T entity, E newEntity) where T : BaseEntity where E : BaseEntity;

        public Task<bool> Delete<T>(T entity) where T : BaseEntity;
    }
}
