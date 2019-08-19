using Microsoft.AspNetCore.Mvc;

namespace Simplicity.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
