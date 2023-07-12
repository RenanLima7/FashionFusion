using Microsoft.EntityFrameworkCore;
using WebLuto.Common.Repository;
using WebLuto.DataContext;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Utils.Messages;

namespace WebLuto.Repositories
{
    public class SaleRepository : BaseRepository, ISaleRepository
    {
        private readonly WLContext _dbContext;

        public SaleRepository(WLContext wLContext) : base(wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<IEnumerable<Sale>> GetAllSaleByClientId(long clientId)
        {
            try
            {
                return await _dbContext.Sale.AsNoTracking()
                    .Where(s => s.ClientId == clientId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
