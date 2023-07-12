using WebLuto.Common.Service;
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
    }
}
