using Microsoft.AspNetCore.Mvc;

namespace Nonamii.Controllers.Inventory
{
    public class StockController : Controller
    {
        private readonly IDeliveriesRepo _deliveriesRepo;
        public StockController(IDeliveriesRepo deliveriesRepo)
        {
            _deliveriesRepo = deliveriesRepo;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetDrivers()
        {
            var drivers = await _deliveriesRepo.GetDriversAsync();
            return View(drivers);
        }
    }
}
