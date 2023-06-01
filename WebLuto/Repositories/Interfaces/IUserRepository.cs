using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> GetUserByUsername(string username);
    }
}
