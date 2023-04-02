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
            try
            {
                return await _dbContext.User.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao buscar todos os usuários! \nErro - {0}", ex));
            }
        }

        public async Task<User> GetUserById(long id)
        {
            try
            {
                return await _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao buscar um usuário com o Id: {0} \nErro - {1}", id, ex));
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
                return await _dbContext.User.FirstOrDefaultAsync(x => x.Username == username);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao buscar um usuário com o Username: {0} \nErro - {1}", username, ex));
            }
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                user.CreationDate = DateTime.Now;

                await _dbContext.User.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao criar o usuário! \nErro - {0}", ex));
            }
        }

        public async Task<User> UpdateUser(User user, long id)
        {
            try
            {
                User userExists = await GetUserById(id);

                if (userExists == null)
                    throw new Exception("Não foi possível encontrar um usuário com o ID: " + id);

                userExists.Username = user.Username;
                userExists.Password = user.Password;
                userExists.Type = user.Type;
                userExists.UpdateDate = DateTime.Now;

                _dbContext.User.Update(userExists);
                await _dbContext.SaveChangesAsync();

                return userExists;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o usuário: {0} \nErro - {1}", id, ex));
            }
        }

        public async Task<bool> DeleteUser(long id)
        {
            try
            {
                User userExists = await GetUserById(id);

                if (userExists == null)
                    throw new Exception("Não foi possível encontrar um usuário com o ID: " + id);

                //_dbContext.User.Remove(userExists);

                userExists.DeletionDate = DateTime.Now;
                _dbContext.User.Update(userExists);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao deletar o usuário: {0} \nErro - {1}", id, ex));
            }
        }
    }
}
