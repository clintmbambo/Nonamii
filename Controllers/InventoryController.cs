using Microsoft.AspNetCore.Mvc;

namespace Nonamii.Controllers
{
    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
