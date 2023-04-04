using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                List<User> userList = await _userRepository.GetAllUsers();

                return userList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserById(long id)
        {
            try
            {
                User user = await _userRepository.GetUserById(id);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
                User user = await _userRepository.GetUserByUsername(username);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> CreateUser(User userToCreate)
        {
            try
            {
                User userCreated = await _userRepository.CreateUser(userToCreate);

                return userCreated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateUser(User userToUpdate, User existingUser)
        {
            try
            {
                User userUpdated = await _userRepository.UpdateUser(userToUpdate, existingUser);

                return userUpdated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(User userToDelete)
        {
            try
            {
                return await _userRepository.DeleteUser(userToDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
