using WebLuto.Common.Service;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class SaleService : BaseService, ISaleService
    {

        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository) : base(saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> GetAllSaleByClientId(long id)
        {
            try
            {
                return await _saleRepository.GetAllSaleByClientId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
