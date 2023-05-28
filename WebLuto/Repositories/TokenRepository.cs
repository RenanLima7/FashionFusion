using Microsoft.EntityFrameworkCore;
using WebLuto.Common.Repository;
using WebLuto.DataContext;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;

namespace WebLuto.Repositories
{
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        private readonly WLContext _dbContext;

        public TokenRepository(WLContext wLContext) : base(wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<ClientToken> GetClientTokenByClientId(long id)
        {
            try
            {
                return await _dbContext.ClientToken.FirstOrDefaultAsync(x => x.ClientId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o token de confirmação - {ex.Message}");
            }
        }

        public async Task<ClientToken> GetClientTokenByToken(string token)
        {
            try
            {
                return await _dbContext.ClientToken.FirstOrDefaultAsync
                (
                    x =>
                    x.Token == token &&
                    x.CreationDate >= DateTime.Now.AddMinutes(-5)
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o token de confirmação - {ex.Message}");
            }
        }
    }
}
