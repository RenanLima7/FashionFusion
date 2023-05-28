namespace WebLuto.Common.Interfaces
{
    public interface IBaseRepository
    {
        public void Create<T>(T entity) where T : class;

        public void Update<T>(T entity) where T : class;

        public void Delete<T>(T entity) where T : class;

        public IEnumerable<T> GetAll<T>() where T : class;

        public T GetById<T>(long id) where T : class;

        bool SaveChanges();
    }
}
