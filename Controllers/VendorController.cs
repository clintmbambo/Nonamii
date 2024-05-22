using Microsoft.AspNetCore.Mvc;

namespace Nonamii.Controllers
{
    [Authorize(Roles = "Restaurant, Admin")]
    public class VendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrdersNav()
        {
            return View();
        }
    }
}
