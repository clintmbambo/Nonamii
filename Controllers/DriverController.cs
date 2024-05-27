using Microsoft.AspNetCore.Mvc;

namespace Nonamii.Controllers
{

    [Authorize]
    public class DriverController : Controller
    {
        private readonly IDeliveriesRepo _deliveriesRepo;

        public DriverController(IDeliveriesRepo deliveriesRepo)
        {
            _deliveriesRepo = deliveriesRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FindDriver(int id)
        {
            var deliveryDetails = await _deliveriesRepo.FindDriver(id);
            return View(deliveryDetails);
        }
    }
}
