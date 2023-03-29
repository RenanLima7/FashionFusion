using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
