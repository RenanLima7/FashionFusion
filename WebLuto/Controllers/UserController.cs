using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Models.DTO;
using WebLuto.Security;
using WebLuto.Services;
using WebLuto.Services.Interfaces;
using WebLuto.Utils;

namespace WebLuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
                    return NotFound(new { Sucess = false, Message = $"Não foi encontrado nemhum usuário com o username: {loginRequest.Username}" });

                bool isValidPassword = Sha512Cryptographer.Compare(loginRequest.Password, user.Salt, user.Password);

                if (isValidPassword)
                {
                    string jwtToken = new TokenService().GenerateToken(user);

                    LoginResponse loginResponse = _mapper.Map<LoginResponse>(user);

                    return Ok(new { Sucess = true, User = loginResponse, Token = jwtToken });
                }
                else
                    return NotFound(new { Sucess = false, Message = "Usuário ou senha inválidos!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Sucess = false, ex.Message, Exception = ex.InnerException });
            }
        }

        [HttpGet]
        [Route("Anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("Authenticated")]
        [Authorize]
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
