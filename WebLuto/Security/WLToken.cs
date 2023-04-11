using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using WebLuto.Services.Interfaces;

namespace WebLuto.Security
{
    public class WLToken : TypeFilterAttribute
    {
        public WLToken() : base(typeof(AuthorizeFilter)) { }

        private class AuthorizeFilter : IAuthorizationFilter
        {
            private readonly ITokenService _tokenService;

            public AuthorizeFilter(ITokenService tokenService)
            {
                _tokenService = tokenService;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                try
                {
                    StringValues authHeader = context.HttpContext.Request.Headers["Authorization"];

                    _tokenService.IsValidToken(authHeader.ToString());
                }
                catch (Exception ex)
                {
                    object obj = new
                    {
                        Title = "Unauthorized",
                        StatusCode = 401,
                        ex.Message
                    };

                    context.Result = new UnauthorizedObjectResult(obj);
                    return;
                }
            }
        }
    }
}
