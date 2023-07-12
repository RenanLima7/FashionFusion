using WebLuto.Common.Repository;
using WebLuto.DataContext;
using WebLuto.Repositories.Interfaces;

namespace WebLuto.Repositories
{
    public class SaleRepository : BaseRepository, ISaleRepository
    {
        private readonly WLContext _dbContext;

        public SaleRepository(WLContext wLContext) : base(wLContext)
        {
            _dbContext = wLContext;
        }
    }
}
