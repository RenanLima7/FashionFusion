using WebLuto.Common.Service;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class ClientService : BaseService, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository) : base(clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            try
            {
                return await _clientRepository.GetClientByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateIsConfirmed(Client client, bool isConfirmed)
        {
            try
            {
                return await _clientRepository.UpdateIsConfirmed(client, isConfirmed);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerifyIsConfirmed(Client client)
        {
            try
            {
                if (client != null)
                    return client.IsConfirmed;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
