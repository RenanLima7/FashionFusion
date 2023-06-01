using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface IAddressRepository : IBaseRepository
    {
        Task<Address> GetAddressByClientIdAsync(long clientId);
    }
}
