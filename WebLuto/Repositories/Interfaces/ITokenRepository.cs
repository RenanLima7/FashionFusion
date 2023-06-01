using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface ITokenRepository : IBaseRepository
    {
        Task<ClientToken> GetClientTokenByClientId(long id);

        Task<ClientToken> GetClientTokenByToken(string token);
    }
}
