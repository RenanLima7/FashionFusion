using Microsoft.EntityFrameworkCore;
using WebLuto.DataContext;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;

namespace WebLuto.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly WLContext _dbContext;

        public TokenRepository(WLContext wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<ClientToken> CreateClientToken(ClientToken clientToken)
        {
            try
            {
                clientToken.CreationDate = DateTime.UtcNow;

                await _dbContext.ClientToken.AddAsync(clientToken);
                await _dbContext.SaveChangesAsync();

                return clientToken;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao salvar o token de confirmação - {ex.Message}"));
            }
        }

        public async Task<ClientToken> GetClientTokenByClientId(long id)
        {
            try
            {
                return await _dbContext.ClientToken.FirstOrDefaultAsync
                (
                x =>
                    x.ClientId == id &&
                    x.DeletionDate == null
                );
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
                    x.DeletionDate == null &&
                    x.CreationDate >= DateTime.UtcNow.AddMinutes(-5)
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o token de confirmação - {ex.Message}");
            }
        }

        public async Task<ClientToken> UpdateClientToken(ClientToken clientToken)
        {
            try
            {
                _dbContext.ClientToken.Update(clientToken);
                await _dbContext.SaveChangesAsync();

                return clientToken;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao atualizar o token de confirmação - {ex.Message}"));
            }
        }
    }
}
