
namespace Nonamii.Repository
{
    public class MenuItemExtrasRepo : IMenuItemExtrasRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public MenuItemExtrasRepo(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public string? GetUserId()
        {
            var principal = _contextAccessor.HttpContext.User;
            string? userId = _userManager.GetUserId(principal);

            return userId;
        }
        public async Task<IEnumerable<MenuItemExtra>> GetMenuItemExtrasAsync()
        {
            var menuExtras = await _db.MenuItemExtras
                .Include(m => m.MenuItem)
                .Include(m => m.Extra)
                .Where(x => x.UserId == GetUserId())
                .ToListAsync();

            return menuExtras;
        }

        
    }
}
