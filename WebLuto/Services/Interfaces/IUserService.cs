using WebLuto.Models;

namespace WebLuto.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(long id);

        Task<User> GetUserByUsername(string username);

        Task<User> CreateUser(User userToCreate);

        Task<User> UpdateUser(User userToUpdate, User existingUser);

        Task<bool> DeleteUser(User userToDelete);
    }
}