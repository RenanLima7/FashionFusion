using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface ITokenService
    {
        Task<ClientToken> GenerateConfirmationCode(Client client);

        string GenerateToken(Client client);

        void IsValidToken(string authorizationHeader);

        bool IsExpiredToken(string token);

        long GetUserIdFromJWTToken(string token);

        bool ExpireToken(string token);

        Task<ClientToken> GetClientTokenByToken(string token);

        Task<ClientToken> GetClientTokenByClientId(long id);

        Task<ClientToken> ResendToken(ClientToken clientToken);
    }
}
