using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.DataContext;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Models.Enums;
using WebLuto.Security;
using WebLuto.Services.Interfaces;

namespace WebLuto.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        private readonly IAddressService _addressService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public ClientController(IMapper mapper, IClientService clientService, IAddressService addressService, ITokenService tokenService, IEmailService emailService)
        {
            _mapper = mapper;
            _clientService = clientService;
            _addressService = addressService;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                Client client = await _clientService.GetClientByEmailOrUsername(loginRequest.Username);

                if (client == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum cliente" });

                bool isConfirmed = _clientService.VerifyIsConfirmed(client);

                if (!isConfirmed)
                    return Unauthorized(new { Success = false, Message = $"Por favor, confirme seu email para prosseguir!" });

                bool isValidPassword = Sha512Cryptographer.Compare(loginRequest.Password, client.Salt, client.Password);

                if (isValidPassword)
                {
                    string jwtToken = _tokenService.GenerateToken(client);

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
        [Route("confirmAccount/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> ConfirmAccount(string token)
        {
            try
            {
                _tokenService.IsValidToken("Bearer " + token);

                long userId = _tokenService.GetUserIdFromJWTToken(token);

                Client client = await _clientService.GetClientById(userId);

                if (client == null)
                    return NotFound(new { Success = false, Message = $"O token de autorização é inválido." });

                bool isConfirmed = _clientService.VerifyIsConfirmed(client);

                if (isConfirmed)
                    return Ok(new { Success = true, Message = $"O Email já foi verificado anteriormente!" });
                else
                    _clientService.UpdateIsConfirmed(client, isConfirmed: true);

                _tokenService.ExpireToken(token);

                return Ok(new { Success = true, Message = "Email confirmado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("getClientById")]
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
        [Route("getAllClients")]
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
        [Route("createClient")]
        public async Task<ActionResult<dynamic>> CreateClient([FromBody] CreateClientRequest clientRequest)
        {
            WLTransaction wLTransaction = new WLTransaction();

            try
            {
                _clientService.ExistsClientWithUsernameOrEmail(clientRequest.Username, clientRequest.Email);

                Client client = _mapper.Map<Client>(clientRequest);
                Client clientCreated = await _clientService.CreateClient(client);

                Address address = _mapper.Map<Address>(clientRequest.Address);
                address.ClientId = clientCreated.Id;
                Address addressCreated = await _addressService.CreateAddress(address);

                string jwtToken = _tokenService.GenerateToken(clientCreated);

                _emailService.SendEmail(clientCreated, EmailTemplateType.AccountCreation, jwtToken);

                CreateClientResponse clientResponse = _mapper.Map<CreateClientResponse>(clientCreated);
                CreateAddressResponse addressResponse = _mapper.Map<CreateAddressResponse>(addressCreated);
                clientResponse.Address = addressResponse;

                wLTransaction.Commit();

                return Ok(new { Success = true, Client = clientResponse });
            }
            catch (Exception ex)
            {
                wLTransaction.Rollback();
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpPut]
        [Route("updateClient")]
        [WLToken]
        public async Task<ActionResult<dynamic>> UpdateClient([FromBody] UpdateClientRequest clientRequest)
        {
            WLTransaction wLTransaction = new WLTransaction();

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

                wLTransaction.Commit();

                return Ok(new { Success = true, Client = clientResponse });
            }
            catch (Exception ex)
            {
                wLTransaction.Rollback();
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpDelete]
        [Route("deleteClient")]
        [WLToken]
        public async Task<ActionResult<dynamic>> DeleteClient()
        {
            WLTransaction wLTransaction = new WLTransaction();

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

                    wLTransaction.Commit();

                    _emailService.SendEmail(existingClient, EmailTemplateType.AccountDeletion);

                    return Ok(new { Success = true, Message = $"Cliente {existingClient.Username} excluído com sucesso!" });
                }
                else
                    return BadRequest(new { Success = false, Message = $"Erro ao excluir o cliente {existingClient.Username}!" });
            }
            catch (Exception ex)
            {
                wLTransaction.Rollback();
                return BadRequest(new { Success = false, ex.Message });
            }
        }
    }
}
