using WebLuto.Common.Interfaces;

namespace WebLuto.Common.Service
{
    public class BaseService : IBaseService
    {
        public IEnumerable<T> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(long id) where T : class
        {
            throw new NotImplementedException();
        }

        public void Create<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
