using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.Models;
using WebLuto.Mapper;
using WebLuto.Models.DTO;
using WebLuto.Models.Enums.UserEnum;
using WebLuto.Services;

namespace WebLuto.Controllers
{
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserDTO userDTO)
        {
            User userteste = new User
            {
                Id = 1,
                Username = "renan@teste",
                Password = "12345678",
                Type = UserType.Admin
            }; // REPOSITORY > GET BY ID

            User user = null;// Mapper.UserMap.MapUserDTOToUser(userDTO);

            if (user == null)
                return NotFound(new { Sucess = false, Message = "Usuário ou senha inválidos!" });

            string token = new TokenService().GenerateToken(user);

            if (token.Contains("ERROR"))
                return BadRequest(new { Sucess = false, Message = "Houve um erro ao gerar o token!", Exception = token });

            user.Password = string.Empty;

            return Ok(
                new
                {
                    Sucess = true,
                    User = user,
                    Token = token
                }
            );

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
