using Microsoft.EntityFrameworkCore;
using WebLuto.DataContext;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Security;
using WebLuto.Utils;

namespace WebLuto.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly WLContext _dbContext;

        public ClientRepository(WLContext wLContext)
        {
            _dbContext = wLContext;
        }

        public async Task<List<Client>> GetAllClients()
        {
            try
            {
                return await _dbContext.Client
                    .Where(x => x.DeletionDate == null)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao buscar todos os clientes! - {ex}"));
            }
        }

        public async Task<Client> GetClientById(long id)
        {
            try
            {
                return await _dbContext.Client.FirstOrDefaultAsync
                (
                    x => x.Id == id &&
                    x.DeletionDate == null
                );
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao buscar o cliente com o Id: {id} - {ex}"));
            }
        }

        public async Task<Client> GetClientByEmailOrUsername(string emailOrUsername)
        {
            try
            {
                return await _dbContext.Client.FirstOrDefaultAsync
                (
                    x => (x.Email == emailOrUsername || x.Username == emailOrUsername) &&
                    x.DeletionDate == null
                );
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao buscar o cliente: {emailOrUsername} - {ex}"));
            }
        }

        public async Task<Client> CreateClient(Client clientToCreate)
        {
            try
            {
                clientToCreate.Email = clientToCreate.Email;
                clientToCreate.Username = clientToCreate.Username;
                clientToCreate.Salt = UtilityMethods.GenerateSalt();
                clientToCreate.Password = Sha512Cryptographer.Encrypt(clientToCreate.Password, clientToCreate.Salt);

                clientToCreate.FirstName = clientToCreate.FirstName;
                clientToCreate.LastName = clientToCreate.LastName;
                clientToCreate.CPF = clientToCreate.CPF;
                clientToCreate.Phone = clientToCreate.Phone;
                clientToCreate.BirthDate = clientToCreate.BirthDate;
                clientToCreate.Avatar = clientToCreate.Avatar;
                clientToCreate.CreationDate = DateTime.Now;
                clientToCreate.IsConfirmed = false;

                await _dbContext.Client.AddAsync(clientToCreate);
                await _dbContext.SaveChangesAsync();

                return clientToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao criar o cliente! - {ex}"));
            }
        }

        public async Task<Client> UpdateClient(Client clientToUpdate, Client existingClient)
        {
            try
            {
                existingClient.Email = clientToUpdate.Email ?? existingClient.Email;
                existingClient.Username = clientToUpdate.Username ?? existingClient.Username;
                existingClient.Password = clientToUpdate.Password != null ? Sha512Cryptographer.Encrypt(clientToUpdate.Password, existingClient.Salt) : existingClient.Password;

                existingClient.FirstName = clientToUpdate.FirstName ?? existingClient.FirstName;
                existingClient.LastName = clientToUpdate.LastName ?? existingClient.LastName;
                existingClient.CPF = clientToUpdate.CPF ?? existingClient.CPF;
                existingClient.Phone = clientToUpdate.Phone ?? existingClient.Phone;
                existingClient.BirthDate = clientToUpdate.BirthDate ?? existingClient.BirthDate;
                existingClient.Avatar = clientToUpdate.Avatar ?? existingClient.Avatar;
                existingClient.UpdateDate = DateTime.Now;

                _dbContext.Client.Update(existingClient);
                await _dbContext.SaveChangesAsync();

                return existingClient;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao atualizar o cliente: {existingClient.Id} - {ex}"));
            }
        }

        public async Task<bool> DeleteClient(Client clientToDelete)
        {
            try
            {
                clientToDelete.DeletionDate = DateTime.Now;

                _dbContext.Client.Update(clientToDelete);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao deletar o cliente: {clientToDelete.Id} - {ex}"));
            }
        }

        public async void UpdateIsConfirmed(Client client, bool isConfirmed)
        {
            try
            {
                client.IsConfirmed = isConfirmed;
                client.UpdateDate = DateTime.Now;

                _dbContext.Client.Update(client);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erro ao confirmar a conta do cliente: {client.Id} - {ex}"));
            }
        }
    }
}
