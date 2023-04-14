using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, string email, long id);

        void IsValidToken(string authorizationHeader);

        bool IsExpiredToken(string token);
    }
}
