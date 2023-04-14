using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClients();

        Task<Client> GetClientById(long id);

        Task<Client> GetClientByEmailOrUsername(string emailOrUsername);

        Task<Client> CreateClient(Client clientToCreate);

        Task<Client> UpdateClient(Client clientToUpdate, Client existingClient);

        Task<bool> DeleteClient(Client clientToDelete);

        void ExistsClientWithUsernameOrEmail(string username, string email);
    }
}
