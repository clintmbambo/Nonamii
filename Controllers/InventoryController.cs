using Microsoft.AspNetCore.Mvc;

namespace Nonamii.Controllers
{
    [Authorize(Roles = "Restaurant")]
    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
