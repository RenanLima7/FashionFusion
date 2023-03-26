using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(User user);

        Task UpdateUser(User user, long id);

        Task DeleteUser(long id);

        Task<User> GetUserById(long id);

        Task<User> GetUserByLogin(string username, string password);
    }
}