using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLuto.Models;
using WebLuto.Models.DTO;
using WebLuto.Models.Enums.UserEnum;
using WebLuto.Security;
using WebLuto.Services;

namespace WebLuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    User user = await _userService.GetUserByUserName(loginDTO.Username);

                    if (user == null)
                        return NotFound(new { Sucess = false, Message = "Usuário não encontrado!" });

                    string token = new TokenService().GenerateToken(user);

                    if (token.Contains("ERROR"))
                        return BadRequest(new { Sucess = false, Message = "Houve um erro ao gerar o token!", Exception = token });

                    bool passwordPwd = Sha512Cryptographer.Compare(loginDTO.Password, user.Salt, user.Password);

                    if (passwordPwd)
                        return Ok(new { Sucess = true, User = user, Token = token });
                    else
                        return NotFound(new { Sucess = false, Message = "Usuário ou senha inválidos!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Sucess = false, Message = "Houve um erro no sistema: " + ex });
            }
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserDTO user)
        {
            return Ok(new { user });
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
