using Microsoft.EntityFrameworkCore;
using WebLuto.Data;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Security;
using WebLuto.Utils;

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
                return await _dbContext.User
                    .Where(x => x.DeletionDate == null)
                    .ToListAsync();
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
                return await _dbContext.User.FirstOrDefaultAsync
                (
                    x => x.Id == id &&
                    x.DeletionDate == null
                );
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
                return await _dbContext.User.FirstOrDefaultAsync
                (
                    x => x.Username == username &&
                    x.DeletionDate == null
                );
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
                user.Salt = UtilityMethods.GenerateSaltAsLong();
                user.Password = Sha512Cryptographer.Encrypt(user.Password, user.Salt);

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
                userExists.Password = Sha512Cryptographer.Encrypt(user.Password, userExists.Salt);
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
