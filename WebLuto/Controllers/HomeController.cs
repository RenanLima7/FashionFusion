using Microsoft.AspNetCore.Mvc;

namespace WebLuto.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult Index()
        {
            return Redirect("/swagger/index.html");
        }
    }
}
