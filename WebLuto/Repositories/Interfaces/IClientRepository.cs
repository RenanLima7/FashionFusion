using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface IClientRepository : IBaseRepository
    {
        Task<Client> GetClientByEmail(string email);

        Task<bool> UpdateIsConfirmed(Client client, bool isConfirmed);
    }
}
