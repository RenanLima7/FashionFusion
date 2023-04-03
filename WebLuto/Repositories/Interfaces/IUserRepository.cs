using WebLuto.DataContract.Requests;
using WebLuto.Models;

namespace WebLuto.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(long id);

        Task<User> GetUserByUsername(string username);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User userToUpdate, User existingUser);

        Task<bool> DeleteUser(User user);
    }
}
