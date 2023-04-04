using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);

        void IsValidToken(string authorizationHeader);

        bool IsExpiredToken(string token);
    }
}
