using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebLuto.Models;
using WebLuto.Security;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class TokenService : ITokenService
    {
        public TokenService() { }

        public string GenerateToken(User user)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                byte[] secretKey = Encoding.ASCII.GetBytes(new Settings().SecretKey);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username.ToString()),
                        new Claim(ClaimTypes.Role, user.Type.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um erro ao gerar o token! \nErro - {ex}");
            }
        }

        public void IsValidToken(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                throw new Exception("Token de autorização inválido");

            string token = authorizationHeader.Substring("Bearer ".Length).Trim();

            if (IsExpiredToken(token))
                throw new Exception("Token de autorização expirado!");
        }

        public bool IsExpiredToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar a validade do token! \nErro - {ex}");
            }
        }
    }
}
