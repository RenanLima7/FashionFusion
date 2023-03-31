using Microsoft.EntityFrameworkCore;
using WebLuto.Data;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;

namespace WebLuto.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WLDBContext _dbContext;

        public UserRepository(WLDBContext wLDBContext)
        {
            _dbContext = wLDBContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            return await _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUserName(string username) 
        { 
            return await _dbContext.User.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> CreateUser(User user)
        {
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(User user, long id)
        {
            User userExists = await GetUserById(id);

            if (userExists == null)
                throw new Exception("Não foi possível encontrar um usuário com o ID: " + id);

            userExists.Username = user.Username;
            userExists.Password = user.Password;
            userExists.Type = user.Type;

            _dbContext.User.Update(userExists);
            await _dbContext.SaveChangesAsync();

            return userExists;
        }

        public async Task<bool> DeleteUser(long id)
        {
            User userExists = await GetUserById(id);

            if (userExists == null)
                throw new Exception("Não foi possível encontrar um usuário com o ID: " + id);

            _dbContext.User.Remove(userExists);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
