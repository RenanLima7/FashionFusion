using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Client client);

        void IsValidToken(string authorizationHeader);

        bool IsExpiredToken(string token);

        long GetUserIdFromJWTToken(string token);

        bool ExpireToken(string token);
    }
}
