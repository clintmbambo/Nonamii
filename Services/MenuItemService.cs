
namespace Nonamii.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public MenuItemService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string? userId = _userManager.GetUserId(principal);
            return userId;
        }
        public async Task<MenuItem> GetMenuItem(string name)
        {
            var menuItem = await _db.MenuItems.
                Where(x => x.Name == name).FirstOrDefaultAsync();

            return menuItem;
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
        {
            string? userId = GetUserId();
            var menuItems = await _db.MenuItems
                .Include(x => x.CartDetails)
                .Include(m => m.MenuItemPromos)
                .Include(m => m.ItemExtras)
                .Include(m => m.ItemSizes)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return menuItems;
        }


    }
}
