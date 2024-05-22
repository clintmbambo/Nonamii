using Microsoft.AspNetCore.Mvc;

namespace Nonamii.Controllers.Inventory
{
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
