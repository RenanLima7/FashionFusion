using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task<ClientToken> CreateClientToken(ClientToken clientToken);

        Task<ClientToken> GetClientTokenByClientId(long id);

        Task<ClientToken> GetClientTokenByToken(string token);

        Task<ClientToken> UpdateClientToken(ClientToken clientToken);
    }
}
