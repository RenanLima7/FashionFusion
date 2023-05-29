using Microsoft.EntityFrameworkCore;
using WebLuto.Common.Repository;
using WebLuto.DataContext;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Security;
using WebLuto.Utils;

namespace WebLuto.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly WLContext _dbContext;

        public UserRepository(WLContext wLContext) : base(wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
                return await _dbContext.User.FirstOrDefaultAsync(x => x.Username == username);
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
                userToCreate.Salt = UtilityMethods.GenerateSalt();
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
    }
}
