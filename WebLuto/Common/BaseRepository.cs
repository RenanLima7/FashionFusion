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

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
