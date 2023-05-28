using Microsoft.EntityFrameworkCore;
using WebLuto.Common;
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
            _dbContext= wLContext;
        }

        public async Task<Address> GetById(long id)
        {
            try
            {
                return await _dbContext.Address.FirstOrDefaultAsync
                (
                    x => x.Id == id &&
                    x.DeletionDate == null
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o endereço com o Id: {id} - {ex.Message}");
            }
        }

        public async Task<Address> GetAddressByClientId(long clientId)
        {
            try
            {
                return await _dbContext.Address.FirstOrDefaultAsync
                (
                    x => x.ClientId == clientId &&
                    x.DeletionDate == null
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o endereço do cliente: {clientId} - {ex.Message}");
            }
        }

        public async Task<Address> Create(Address addressToCreate)
        {
            try
            {
                addressToCreate.ClientId = addressToCreate.ClientId;
                addressToCreate.ZipCode = addressToCreate.ZipCode;
                addressToCreate.AddressLine = addressToCreate.AddressLine;
                addressToCreate.AddressLineNumber = addressToCreate.AddressLineNumber;
                addressToCreate.Neighborhood = addressToCreate.Neighborhood;
                addressToCreate.CreationDate = DateTime.UtcNow;

                await _dbContext.Address.AddAsync(addressToCreate);
                await _dbContext.SaveChangesAsync();

                return addressToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar o endereço - {ex.Message}");
            }
        }

        public async Task<Address> Update(Address addressToUpdate, Address existingAddress)
        {
            try
            {
                existingAddress.ZipCode = addressToUpdate.ZipCode ?? existingAddress.ZipCode;
                existingAddress.AddressLine = addressToUpdate.AddressLine ?? existingAddress.AddressLine;
                existingAddress.AddressLineNumber = addressToUpdate.AddressLineNumber ?? existingAddress.AddressLineNumber;
                existingAddress.Neighborhood = addressToUpdate.Neighborhood ?? existingAddress.Neighborhood;
                existingAddress.UpdateDate = DateTime.UtcNow;

                _dbContext.Address.Update(existingAddress);
                await _dbContext.SaveChangesAsync();

                return existingAddress;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o endereço: {existingAddress.Id} - {ex.Message}");
            }
        }

        public async Task<bool> Delete(Address addressToDelete)
        {
            try
            {
                addressToDelete.DeletionDate = DateTime.UtcNow;

                _dbContext.Address.Update(addressToDelete);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar o endereço: {addressToDelete.Id} - {ex.Message}");
            }
        }
    }
}
