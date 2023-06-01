using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IAddressService : IBaseService
    {
        Task<Address> GetAddressByClientId(long clientId);
    }
}
