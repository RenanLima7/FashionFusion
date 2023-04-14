using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> GetAddressById(long id)
        {
            try
            {
                return await _addressRepository.GetAddressById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> GetAddressByClientId(long clientId)
        {
            try
            {
                return await _addressRepository.GetAddressById(clientId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> CreateAddress(Address addressToCreate)
        {
            try
            {
                return await _addressRepository.CreateAddress(addressToCreate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> UpdateAddress(Address addressToUpdate, Address existingAddress)
        {
            try
            {
                return await _addressRepository.UpdateAddress(addressToUpdate, existingAddress);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAddress(Address addressToDelete)
        {
            try
            {
                return await _addressRepository.DeleteAddress(addressToDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
