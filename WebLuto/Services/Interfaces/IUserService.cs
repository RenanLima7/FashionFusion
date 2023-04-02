using WebLuto.DataContract.Requests;
using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(long id);

        Task<User> GetUserByUsername(string username);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user, long id);

        Task<bool> DeleteUser(long id);
    }
}