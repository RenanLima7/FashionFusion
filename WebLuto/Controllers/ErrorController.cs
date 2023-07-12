using Microsoft.AspNetCore.Mvc;
using WebLuto.Utils.Messages;

namespace WebLuto.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public async Task<ActionResult<dynamic>> Error()
        {
            object response = new
            {
                Success = false,
                Entity = new { },
                Message = ApiMsg.EXC0002
            };

            return BadRequest(response);
        }
    }
}
