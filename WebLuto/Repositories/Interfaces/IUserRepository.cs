using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(long id);

        Task<User> GetUserByUserName(string username);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user, long id);

        Task<bool> DeleteUser(long id);
    }
}
