using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebLuto.Models;
using WebLuto.Repositories.Interfaces;
using WebLuto.Security;
using WebLuto.Services.Interfaces;

namespace WebLuto.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<ClientToken> GenerateConfirmationCode(Client client)
        {
            try
            {
                string token = new Random().Next(1111, 9999).ToString();

                ClientToken clientToken = new ClientToken
                {
                    //Client = client,
                    Token = token,
                    CreationDate= DateTime.Now
                };

                await _tokenRepository.Create(clientToken);

                return clientToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateToken(Client client)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                byte[] secretKey = Encoding.ASCII.GetBytes(new Settings().SecretKey);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, client.Email),
                        new Claim("UserId", client.Id.ToString())
                    }),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um erro ao gerar o token - {ex.Message}");
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

                return jwtToken.ValidTo < DateTime.Now;
            }
            catch (Exception)
            {
                throw new SecurityTokenInvalidSignatureException();
            }
        }

        public long GetUserIdFromJWTToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

                string userid = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                return long.Parse(userid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ExpireToken(string token)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new Settings().SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secretKey,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var expirationDateUnix = long.Parse(claimsPrincipal.FindFirst("exp").Value);
                var expirationDateUtc = DateTimeOffset.FromUnixTimeSeconds(expirationDateUnix).UtcDateTime;

                var newTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsPrincipal.Identity as ClaimsIdentity,
                    Expires = DateTime.Now.AddSeconds(1),
                    SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var newToken = tokenHandler.CreateToken(newTokenDescriptor);
                var newTokenString = tokenHandler.WriteToken(newToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ClientToken> GetClientTokenByToken(string token)
        {
            try
            {
                return await _tokenRepository.GetClientTokenByToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientToken> GetClientTokenByClientId(long id)
        {
            try
            {
                return await _tokenRepository.GetClientTokenByClientId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientToken> ResendToken(ClientToken clientToken)
        {
            string token = new Random().Next(1111, 9999).ToString();

            ClientToken newClientToken = new ClientToken
            {
                //Client = clientToken.Client,
                Token = token,
                CreationDate = clientToken.CreationDate,
                UpdateDate = DateTime.Now
            };

            return await _tokenRepository.Update(clientToken, newClientToken);
        }
    }
}
