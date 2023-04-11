using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Security;
using WebLuto.Services.Interfaces;

namespace WebLuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                //TryValidateModel(loginRequest);

                //if (!ModelState.IsValid)
                //{
                //    UtilityMethods.GetFieldsErrors(ModelState);
                //}

                User user = await _userService.GetUserByUsername(loginRequest.Username);

                if (user == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum usuário com o username: {loginRequest.Username}" });

                bool isValidPassword = Sha512Cryptographer.Compare(loginRequest.Password, user.Salt, user.Password);

                if (isValidPassword)
                {
                    string jwtToken = _tokenService.GenerateToken(user);

                    LoginResponse loginResponse = _mapper.Map<LoginResponse>(user);

                    return Ok(new { Success = true, User = loginResponse, Token = jwtToken });
                }
                else
                    return BadRequest(new { Success = false, Message = "Usuário ou senha inválidos!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<ActionResult<dynamic>> GetUserById([FromQuery] long userId)
        {
            try
            {
                User user = await _userService.GetUserById(userId);

                if (user == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum usuário com o Id: {userId}" });
                else
                {
                    CreateUserResponse userResponse = _mapper.Map<CreateUserResponse>(user);

                    return Ok(new { Success = true, User = userResponse });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<dynamic>> GetAllUsers()
        {
            try
            {
                List<User> userList = await _userService.GetAllUsers();

                if (userList == null || userList.Count == 0)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum usuário" });
                else
                {
                    List<CreateUserResponse> userResponseList = new List<CreateUserResponse>();

                    foreach (User user in userList)
                    {
                        userResponseList.Add(_mapper.Map<CreateUserResponse>(user));
                    }

                    return Ok(new { Success = true, UserList = userResponseList });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        [Authorize] 
        public async Task<ActionResult<dynamic>> CreateUser([FromBody] CreateUserRequest userRequest)
        {
            try
            {
                User userExists = await _userService.GetUserByUsername(userRequest.Username);

                if (userExists != null)
                    return Conflict(new { Success = false, Message = $"Já existe um usuário com o username: {userRequest.Username}" });
                else
                {
                    User user = _mapper.Map<User>(userRequest);

                    User userCreated = await _userService.CreateUser(user);

                    CreateUserResponse userResponse = _mapper.Map<CreateUserResponse>(userCreated);

                    return Ok(new { Success = true, User = userResponse });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<ActionResult<dynamic>> UpdateUser([FromBody] UpdateUserRequest userRequest, long id)
        {
            try
            {
                User existingUser = await _userService.GetUserById(id);

                if (existingUser == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum usuário com o Id: {id}" });

                if (!string.IsNullOrEmpty(userRequest.Username))
                {
                    User userByUsername = await _userService.GetUserByUsername(userRequest.Username);

                    // TODO: Validação para caso seja o próprio usuário
                    if (userByUsername != null)
                        return Conflict(new { Success = false, Message = $"Já existe um usuário com o username: {userRequest.Username}" });
                }

                User userToUpdated = _mapper.Map<User>(userRequest);

                User userUpdated = await _userService.UpdateUser(userToUpdated, existingUser);

                UpdateUserResponse userResponse = _mapper.Map<UpdateUserResponse>(userUpdated);

                return Ok(new { Success = true, User = userResponse });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<ActionResult<dynamic>> DeleteUser(long id)
        {
            try
            {
                User existingUser = await _userService.GetUserById(id);

                if (existingUser == null)
                    return NotFound(new { Success = false, Message = $"Não foi encontrado nenhum usuário com o Id: {id}" });

                bool successDeletedUser = await _userService.DeleteUser(existingUser);

                if (successDeletedUser)
                    return Ok(new { Success = true, User = $"Usuário {existingUser.Id} excluído com sucesso!" });
                else
                    return BadRequest(new { Success = false, User = $"Erro ao excluir o usuário {existingUser.Id}!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        #region Authentication Examples

        /*
        [HttpGet]
        [Route("Anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("Authenticated")]
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("Client")]
        [Authorize(Roles = "0, 1, 2")]
        public string Client() => "Cliente";

        [HttpGet]
        [Route("Employee")]
        [Authorize(Roles = "0, 1")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "0, 1")]
        public string Admin() => "Administrador";
        */

        #endregion
    }
}
