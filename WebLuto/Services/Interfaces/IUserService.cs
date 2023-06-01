using WebLuto.Common.Interfaces;
using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<User> GetUserByUsername(string username);
    }
}