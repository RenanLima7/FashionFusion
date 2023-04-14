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
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um erro ao gerar o token - {ex}");
            }
        }

        public void IsValidToken(string authorizationHeader)
        {
            try
            {
                if (string.IsNullOrEmpty(authorizationHeader))
                    throw new SecurityTokenInvalidSignatureException();

                if (!authorizationHeader.StartsWith("Bearer "))
                    throw new SecurityTokenInvalidSigningKeyException();

                string token = authorizationHeader.Substring("Bearer ".Length).Trim();

                if (IsExpiredToken(token))
                    throw new SecurityTokenExpiredException();
            }
            catch (Exception ex)
            {
                string message;

                switch (ex)
                {
                    case SecurityTokenInvalidSignatureException:
                        message = "O token de autorização é inválido.";
                        break;
                    case SecurityTokenInvalidSigningKeyException:
                        message = "O token de autorização deve conter o prefixo \"Bearer \".";
                        break;
                    case SecurityTokenExpiredException:
                        message = "O token de autorização expirou.";
                        break;
                    default:
                        message = "Ocorreu um erro ao processar a solicitação.";
                        break;
                }

                throw new Exception(message);
            }
        }

        public bool IsExpiredToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch (Exception)
            {
                throw new SecurityTokenInvalidSignatureException();
            }
        }
    }
}
