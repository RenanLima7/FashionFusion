using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLuto.DataContext;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Models.Enums;
using WebLuto.Security;
using WebLuto.Services.Interfaces;
using WebLuto.Utils;
using WebLuto.Utils.Messages;

namespace WebLuto.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private static object Entity;

        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        private readonly IAddressService _addressService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;

        public ClientController(IMapper mapper, IClientService clientService, IAddressService addressService, ITokenService tokenService, IEmailService emailService, IFileService fileService)
        {
            _mapper = mapper;
            _clientService = clientService;
            _addressService = addressService;
            _tokenService = tokenService;
            _emailService = emailService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("me")]
        [WLToken]
        public async Task<ActionResult<dynamic>> Me()
        {
            try
            {
                Client client = await _clientService.GetClientByEmail(User.Identity.Name);

                if (client == null)
                    return Unauthorized(new { Success = false, Message = TokenMsg.EXC0001 });
                else
                {
                    LoginClientResponse loginResponse = _mapper.Map<LoginClientResponse>(client);

                    Entity = new { Client = loginResponse };

                    return Ok(new { Success = true, Entity, Message = ApiMsg.INF0001 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                Client client = await _clientService.GetClientByEmail(loginRequest.Email);

                if (client == null)
                    return NotFound(new { Success = false, Entity = new { }, Message = ClientMsg.EXC0001 });

                bool isValidPassword = Sha512Cryptographer.Compare(loginRequest.Password, client.Salt, client.Password);

                if (isValidPassword)
                {
                    string jwtToken = _tokenService.GenerateToken(client);

                    LoginClientResponse loginResponse = _mapper.Map<LoginClientResponse>(client);

                    Entity = new { Client = loginResponse, Token = jwtToken };

                    return Ok(new { Success = true, Entity, Message = ApiMsg.INF0001 });
                }
                else
                    return BadRequest(new { Success = false, Entity = new { }, Message = ApiMsg.EXC0001 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
            }
        }

        [HttpGet]
        [Route("confirmAccount/{token}")]
        public async Task<ActionResult<dynamic>> ConfirmAccount(string token)
        {
            try
            {
                ClientToken clientToken = await _tokenService.GetClientTokenByToken(token);

                if (clientToken == null)
                    return NotFound(new { Success = false, Entity = new { }, Message = TokenMsg.EXC0001 });

                Client client = await _clientService.GetByIdAsync<Client>(15);//clientToken.ClientId);

                if (client == null)
                    return NotFound(new { Success = false, Entity = new { }, Message = ClientMsg.EXC0001 });

                bool isConfirmed = _clientService.VerifyIsConfirmed(client);

                if (isConfirmed)
                    return Ok(new { Success = true, Entity = new { client.Email }, Message = EmailMsg.EXC0002 });
                else
                    await _clientService.UpdateIsConfirmed(client, isConfirmed: true);

                _emailService.SendEmail(client, EmailTemplateType.ConfirmAccountCreation);

                return Ok(new { Success = true, Entity = new { client.Email }, Message = EmailMsg.INF0001 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
            }
        }

        [HttpPost]
        [Route("resendToken")]
        [WLToken]
        public async Task<ActionResult<dynamic>> ResendToken()
        {
            try
            {
                Client client = await _clientService.GetClientByEmail(User.Identity.Name);

                if (client == null)
                    return NotFound(new { Success = false, Entity = new { }, Message = ClientMsg.EXC0001 });

                bool isConfirmed = _clientService.VerifyIsConfirmed(client);

                if (isConfirmed)
                    return Ok(new { Success = true, Entity = new { client.Email }, Message = EmailMsg.EXC0002 });

                string token;

                ClientToken clientToken = await _tokenService.GetClientTokenByClientId(client.Id);

                if (clientToken == null)
                    token = _tokenService.GenerateConfirmationCode(client).Result.Token;
                else if (clientToken.CreationDate >= DateTime.Now.AddMinutes(-5))
                    token = clientToken.Token;
                else
                    token = _tokenService.ResendToken(clientToken).Result.Token;

                _emailService.SendEmail(client, EmailTemplateType.EmailConfirmation, token);

                return Ok(new { Success = true, Entity = new { client.Email }, Message = EmailMsg.INF0002 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
            }
        }

        [HttpGet]
        [Route("getAllClients")]
        [WLToken]
        public async Task<ActionResult<dynamic>> GetAllClients()
        {
            try
            {
                IEnumerable<Client> clientList = await _clientService.GetAllAsync<Client>(); // ToDo - Paginação

                if (!clientList.Any())
                    return NotFound(new { Success = false, Entity = new { }, Message = ClientMsg.EXC0001 });
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

                    Entity = new { ClientList = clientResponseList };

                    return Ok(new { Success = true, Entity, Message = ApiMsg.INF0002 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
            }
        }

        [HttpPost]
        [Route("createClient")]
        public async Task<ActionResult<dynamic>> CreateClient([FromBody] CreateClientRequest clientRequest)
        {
            WLTransaction wLTransaction = new WLTransaction();

            try
            {
                Client clientExists = await _clientService.GetClientByEmail(clientRequest.Email);

                if (clientExists != null && clientExists.IsConfirmed)
                    return Conflict(new { Success = false, Entity = new { }, Message = string.Format(ClientMsg.EXC0002, clientRequest.Email) });

                Client client = _mapper.Map<Client>(clientRequest);

                if (!string.IsNullOrEmpty(client.Avatar))
                    client.Avatar = _fileService.UploadBase64Image(client.Avatar, "images");

                client.Salt = UtilityMethods.GenerateSalt();
                client.Password = Sha512Cryptographer.Encrypt(client.Password, client.Salt);
                client.IsConfirmed = false;

                Client clientCreated = await _clientService.Create(client);

                Address address = _mapper.Map<Address>(clientRequest.Address);
                address.ClientId = clientCreated.Id;
                Address addressCreated = await _addressService.Create(address);

                ClientToken clientToken = await _tokenService.GenerateConfirmationCode(clientCreated);

                _emailService.SendEmail(clientCreated, EmailTemplateType.EmailConfirmation, clientToken.Token);

                CreateClientResponse clientResponse = _mapper.Map<CreateClientResponse>(clientCreated);
                CreateAddressResponse addressResponse = _mapper.Map<CreateAddressResponse>(addressCreated);
                clientResponse.Address = addressResponse;

                wLTransaction.Commit();

                Entity = new { Client = clientResponse };

                return Ok(new { Success = true, Entity, Message = ClientMsg.INF0001 });
            }
            catch (Exception ex)
            {
                wLTransaction.Rollback();
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
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
                Client existingClient = await _clientService.GetClientByEmail(User.Identity.Name);

                if (existingClient == null)
                    return NotFound(new { Success = false, Entity = new { }, Message = ClientMsg.EXC0001 });

                if (!string.IsNullOrEmpty(clientRequest.Email))
                {
                    Client clientEmail = await _clientService.GetClientByEmail(clientRequest.Email);

                    if (clientEmail != null)
                        return Conflict(new { Success = false, Entity = new { }, Message = string.Format(ClientMsg.EXC0002, clientRequest.Email) });
                }

                Client clientUpdated = _mapper.Map<Client>(clientRequest);

                if (!string.IsNullOrEmpty(clientRequest.Password))
                {
                    clientUpdated.Password = Sha512Cryptographer.Encrypt(clientUpdated.Password, existingClient.Salt);
                }

                if (existingClient.Avatar != null)
                {
                    /*
                    if (existingClient.Avatar != null)
                        clientToUpdated.Avatar = _fileService.UpdateImageStorage(existingClient.Avatar, clientToUpdated.Avatar, "images");
                    else
                        clientToUpdated.Avatar = _fileService.UploadBase64Image(clientToUpdated.Avatar, "images");
                    */
                }

                clientUpdated = await _clientService.Update(existingClient, clientUpdated);

                Address existingAddress = await _addressService.GetAddressByClientId(existingClient.Id);

                if (clientRequest.Address != null)
                {
                    Address addressToUpdated = _mapper.Map<Address>(clientRequest.Address);
                    existingAddress = await _addressService.Update(existingAddress, addressToUpdated);
                }

                if (existingClient.IsConfirmed)
                    _emailService.SendEmail(clientUpdated, EmailTemplateType.AccountUpdate);

                UpdateAddressResponse addressResponse = _mapper.Map<UpdateAddressResponse>(existingAddress);

                UpdateClientResponse clientResponse = _mapper.Map<UpdateClientResponse>(clientUpdated);
                clientResponse.Address = addressResponse;

                wLTransaction.Commit();

                Entity = new { Client = clientResponse };

                return Ok(new { Success = true, Entity, Message = ClientMsg.INF0002 });
            }
            catch (Exception ex)
            {
                wLTransaction.Rollback();
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
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
                Client existingClient = await _clientService.GetClientByEmail(User.Identity.Name);

                if (existingClient == null)
                    return NotFound(new { Success = false, Entity = new { }, Message = ClientMsg.EXC0001 });

                await _clientService.Delete(existingClient);

                Address address = await _addressService.GetAddressByClientId(existingClient.Id);

                if (address != null)
                    await _addressService.Delete(address);

                // if (existingClient.Avatar != null) _fileService.DeleteImageStorage(existingClient.Avatar, "images");

                if (existingClient.IsConfirmed)
                    _emailService.SendEmail(existingClient, EmailTemplateType.AccountDeletion);

                wLTransaction.Commit();

                return Ok(new { Success = true, Entity = new { }, Message = ClientMsg.INF0003 });
            }
            catch (Exception ex)
            {
                wLTransaction.Rollback();
                return BadRequest(new { Success = false, Entity = new { }, ex.Message });
            }
        }
    }
}
