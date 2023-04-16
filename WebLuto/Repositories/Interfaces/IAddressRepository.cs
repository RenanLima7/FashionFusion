using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressById(long id);

        Task<Address> GetAddressByClientId(long clientId);

        Task<Address> CreateAddress(Address addressToCreate);

        Task<Address> UpdateAddress(Address addressToUpdate, Address existingAddress);

        Task<bool> DeleteAddress(Address addressToDelete);
    }
}
