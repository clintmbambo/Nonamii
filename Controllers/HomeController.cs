using System.Diagnostics;

namespace Nonamii.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepo _homeRepo;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, IHomeRepo homeRepo, ApplicationDbContext db)
        {
            _logger = logger;
            _homeRepo = homeRepo;
            _db = db;
        }

        public async Task<IActionResult> Index(string searchTerm="")
        {
            var menuItems = await _homeRepo.GetMenuItems(searchTerm);
            var menuItemViewModel = new MenuItemViewModel
            {
                MenuItems = menuItems,
                SearchTerm = searchTerm,
            };

            return View(menuItemViewModel);
        }

        public async Task<IActionResult> Landing()
        {
            return View();
        }

        public async Task<IActionResult> MenuItemDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _db.MenuItems
                .Include(x => x.ItemSizes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        public async Task<IActionResult> Contact()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
