using WebLuto.Models;
using WebLuto.Repositories;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user, long id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByLogin(string username, string password)
        {
            try
            {
                Task<User> user = _userRepository.GetUserByLogin(username, password);

                return user;
            }
            catch (Exception)
            {
                return null;
            }            
        }
    }
}
