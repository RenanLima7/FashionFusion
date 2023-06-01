using Microsoft.EntityFrameworkCore;
using WebLuto.Common.Repository;
using WebLuto.DataContext;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Utils.Messages;

namespace WebLuto.Repositories
{
    public class ClientRepository : BaseRepository, IClientRepository
    {
        private readonly WLContext _dbContext;

        public ClientRepository(WLContext wLContext) : base(wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            try
            {
                return await _dbContext.Client.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(ClientMsg.EXC0004, email, ex.Message));
            }
        }

        public async Task<bool> UpdateIsConfirmed(Client client, bool isConfirmed)
        {
            try
            {
                client.IsConfirmed = isConfirmed;
                client.UpdateDate = DateTime.Now;

                _dbContext.Client.Update(client);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(ClientMsg.EXC0007, ex.Message));
            }
        }
    }
}
