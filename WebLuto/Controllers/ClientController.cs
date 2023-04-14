using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Security;
using WebLuto.Services.Interfaces;

namespace WebLuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        private readonly IMapper _mapper;

        private readonly IAddressService _addressService;

        private readonly ITokenService _tokenService;

        public ClientController(IClientService clientService, IMapper mapper, IAddressService addressService, ITokenService tokenService)
        {
            _clientService = clientService;
            _mapper = mapper;
            _addressService = addressService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                Client client = await _clientService.GetClientByEmailOrUsername(loginRequest.Username);

                if (client == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum cliente" });

                bool isValidPassword = Sha512Cryptographer.Compare(loginRequest.Password, client.Salt, client.Password);

                if (isValidPassword)
                {
                    string jwtToken = _tokenService.GenerateToken(client.Username, client.Email, client.Id);

                    Address address = await _addressService.GetAddressByClientId(client.Id);
                    CreateAddressResponse addressResponse = _mapper.Map<CreateAddressResponse>(address);

                    LoginClientResponse loginResponse = _mapper.Map<LoginClientResponse>(client);
                    loginResponse.Address = addressResponse;

                    return Ok(new { Success = true, Client = loginResponse, Token = jwtToken });
                }
                else
                    return BadRequest(new { Success = false, Message = "Cliente ou senha inválidos!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("GetClientById")]
        [WLToken]
        public async Task<ActionResult<dynamic>> GetClientById()
        {
            try
            {
                Client client = await _clientService.GetClientByEmailOrUsername(User.Identity.Name);

                if (client == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum cliente!" });
                else
                {
                    Address address = await _addressService.GetAddressByClientId(client.Id);
                    CreateAddressResponse addressResponse = _mapper.Map<CreateAddressResponse>(address);

                    CreateClientResponse clientResponse = _mapper.Map<CreateClientResponse>(client);
                    clientResponse.Address = addressResponse;

                    return Ok(new { Success = true, Client = clientResponse });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllClients")]
        [WLToken]
        public async Task<ActionResult<dynamic>> GetAllClients()
        {
            try
            {
                List<Client> clientList = await _clientService.GetAllClients();

                if (clientList == null || clientList.Count == 0)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum cliente" });
                else
                {
                    List<CreateClientResponse> clientResponseList = new List<CreateClientResponse>();

                    foreach (Client client in clientList)
                    {
                        Address address = await _addressService.GetAddressByClientId(client.Id);
                        CreateAddressResponse addressResponse = _mapper.Map<CreateAddressResponse>(address);

                        CreateClientResponse clientResponse = _mapper.Map<CreateClientResponse>(client);
                        clientResponse.Address = addressResponse;

                        clientResponseList.Add(clientResponse);
                    }

                    return Ok(new { Success = true, ClientList = clientResponseList });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("CreateClient")]
        public async Task<ActionResult<dynamic>> CreateClient([FromBody] CreateClientRequest clientRequest)
        {
            try
            {
                _clientService.ExistsClientWithUsernameOrEmail(clientRequest.Username, clientRequest.Email);

                Client client = _mapper.Map<Client>(clientRequest);
                Client clientCreated = await _clientService.CreateClient(client);

                Address address = _mapper.Map<Address>(clientRequest.Address);
                address.ClientId = clientCreated.Id;
                Address addressCreated = await _addressService.CreateAddress(address);

                string jwtToken = _tokenService.GenerateToken(clientCreated.Username, clientCreated.Email, clientCreated.Id);

                CreateClientResponse clientResponse = _mapper.Map<CreateClientResponse>(clientCreated);
                CreateAddressResponse addressResponse = _mapper.Map<CreateAddressResponse>(addressCreated);
                clientResponse.Address = addressResponse;

                return Ok(new { Success = true, Client = clientResponse, Token = jwtToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateClient")]
        [WLToken]
        public async Task<ActionResult<dynamic>> UpdateClient([FromBody] UpdateClientRequest clientRequest)
        {
            try
            {
                // Refresh Token
                Client existingClient = await _clientService.GetClientByEmailOrUsername(User.Identity.Name);

                if (existingClient == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum cliente" });

                if (!string.IsNullOrEmpty(clientRequest.Username) || !string.IsNullOrEmpty(clientRequest.Email))
                    _clientService.ExistsClientWithUsernameOrEmail(clientRequest.Username, clientRequest.Email);

                Client clientToUpdated = _mapper.Map<Client>(clientRequest);
                Client clientUpdated = await _clientService.UpdateClient(clientToUpdated, existingClient);

                Address existingAddress = await _addressService.GetAddressByClientId(existingClient.Id);

                if (clientRequest.Address != null)
                {
                    Address addressToUpdated = _mapper.Map<Address>(clientRequest.Address);
                    existingAddress = await _addressService.UpdateAddress(addressToUpdated, existingAddress);
                }

                UpdateAddressResponse addressResponse = _mapper.Map<UpdateAddressResponse>(existingAddress);

                UpdateClientResponse clientResponse = _mapper.Map<UpdateClientResponse>(clientUpdated);
                clientResponse.Address = addressResponse;                

                return Ok(new { Success = true, Client = clientResponse });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteClient")]
        [WLToken]
        public async Task<ActionResult<dynamic>> DeleteClient()
        {
            try
            {
                Client existingClient = await _clientService.GetClientByEmailOrUsername(User.Identity.Name);

                if (existingClient == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum cliente!" });

                bool successDeletedClient = await _clientService.DeleteClient(existingClient);

                if (successDeletedClient)
                {
                    Address address = await _addressService.GetAddressByClientId(existingClient.Id);
                    _addressService.DeleteAddress(address);

                    return Ok(new { Success = true, Message = $"Cliente {existingClient.Username} excluído com sucesso!" });
                }
                else
                    return BadRequest(new { Success = false, Message = $"Erro ao excluir o cliente {existingClient.Username}!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }
    }
}
