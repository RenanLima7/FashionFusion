using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface ISaleService : IBaseService
    {
        Task<IEnumerable<Sale>> GetAllSaleByClientId(long id);
    }
}