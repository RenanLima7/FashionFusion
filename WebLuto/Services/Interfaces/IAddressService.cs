using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IAddressService
    {
        Task<Address> GetAddressById(long id);

        Task<Address> GetAddressByClientId(long clientId);

        Task<Address> CreateAddress(Address addressToCreate);

        Task<Address> UpdateAddress(Address addressToUpdate, Address existingAddress);

        Task<bool> DeleteAddress(Address addressToDelete);
    }
}
