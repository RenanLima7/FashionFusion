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
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao buscar todos os usuários!"));
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
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao buscar um usuário com o Id: {0}", id));
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
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao buscar um usuário com o Username: {0}", username));
            }
        }

        public async Task<User> CreateUser(User userToCreate)
        {
            try
            {
                userToCreate.CreationDate = DateTime.Now;
                userToCreate.Salt = UtilityMethods.GenerateSaltAsLong();
                userToCreate.Password = Sha512Cryptographer.Encrypt(userToCreate.Password, userToCreate.Salt);

                await _dbContext.User.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();

                return userToCreate;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao criar o usuário!"));
            }
        }

        public async Task<User> UpdateUser(User userToUpdate, User existingUser)
        {
            try
            {                
                existingUser.Username = userToUpdate.Username ?? existingUser.Username;
                existingUser.Password = userToUpdate.Password != null ? Sha512Cryptographer.Encrypt(userToUpdate.Password, existingUser.Salt) : existingUser.Password;
                existingUser.Type = userToUpdate.Type != existingUser.Type ? userToUpdate.Type : existingUser.Type;
                existingUser.UpdateDate = DateTime.Now;

                _dbContext.User.Update(existingUser);
                await _dbContext.SaveChangesAsync();

                return existingUser;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao atualizar o usuário: {0}", existingUser.Id));
            }
        }

        public async Task<bool> DeleteUser(User userToDelete) 
        {
            try
            {
                //_dbContext.User.Remove(userToDelete);

                userToDelete.DeletionDate = DateTime.Now;

                _dbContext.User.Update(userToDelete);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Erro ao deletar o usuário: {0}", userToDelete.Id));
            }
        }
    }
}
