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
                return await _userRepository.GetAllUsers();
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
                return await _userRepository.GetUserById(id);
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
                return await _userRepository.GetUserByUsername(username);
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
                return await _userRepository.CreateUser(userToCreate);
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
                return await _userRepository.UpdateUser(userToUpdate, existingUser);
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
