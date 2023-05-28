using WebLuto.Common.Repository;
using WebLuto.DataContext;
using WebLuto.Repositories.Interfaces;

namespace WebLuto.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly WLContext _dbContext;

        public ProductRepository(WLContext wLContext) : base(wLContext)
        {
            _dbContext = wLContext;
        }
    }
}
