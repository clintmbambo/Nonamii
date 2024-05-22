using Microsoft.AspNetCore.Mvc;

namespace Nonamii.Controllers
{

    [Authorize(Roles = "Driver")]
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
