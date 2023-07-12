using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface ISaleRepository : IBaseRepository
    {
        Task<IEnumerable<Sale>> GetAllSaleByClientId(long clientId);
    }
}