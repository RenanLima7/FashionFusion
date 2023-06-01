using WebLuto.Common.Service;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository) : base(addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> GetAddressByClientId(long clientId)
        {
            try
            {
                return await _addressRepository.GetAddressByClientIdAsync(clientId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
