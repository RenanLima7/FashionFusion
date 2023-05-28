using Microsoft.EntityFrameworkCore;
using WebLuto.Common.Repository;
using WebLuto.DataContext;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;

namespace WebLuto.Repositories
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        private readonly WLContext _dbContext;

        public AddressRepository(WLContext wLContext) : base(wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<Address> GetAddressByClientIdAsync(long clientId)
        {
            try
            {
                return await _dbContext.Address
                    .Where(x => x.ClientId == clientId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o endereço do cliente: {clientId} - {ex.Message}");
            }
        }
    }
}
