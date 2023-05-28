using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IClientService : IBaseService
    {
        Task<Client> GetClientByEmail(string email);

        Task<bool> UpdateIsConfirmed(Client client, bool isConfirmed);

        bool VerifyIsConfirmed(Client client);
    }
}
