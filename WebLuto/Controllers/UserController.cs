using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Security;
using WebLuto.Services;
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
                    UserResponse userResponse = _mapper.Map<UserResponse>(user);

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
                    List<UserResponse> userResponseList = new List<UserResponse>();

                    foreach (User user in userList)
                    {
                        userResponseList.Add(_mapper.Map<UserResponse>(user));
                    }

                    return Ok(new { Success = true, UserList = userResponseList });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, ex.Message, Exception = ex.InnerException });
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> CreateUser([FromBody] UserRequest userRequest)
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

                    UserResponse userResponse = _mapper.Map<UserResponse>(userCreated);

                    return Ok(new { Success = true, User = userResponse });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message, Exception = ex });
            }
        }

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
    }
}
