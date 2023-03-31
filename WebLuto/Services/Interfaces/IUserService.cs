using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(long id);

        Task<User> GetUserByUserName(string username);

        Task CreateUser(User user);

        Task UpdateUser(User user, long id);

        Task DeleteUser(long id);
    }
}