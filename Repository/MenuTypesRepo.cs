
namespace Nonamii.Repository
{
    public class MenuTypesRepo : IMenuTypesRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public MenuTypesRepo(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string? GetUserId()
        {
            var httpUser = _contextAccessor.HttpContext.User;
            string? userId = _userManager.GetUserId(httpUser);

            return userId;
        }
        public async Task<IEnumerable<MenuType>> GetMenuTypesAsync()
        {
            string? userId = GetUserId();

            var menuTypes = await _db.MenuType
                .Include(m => m.Menus)
                .Where(m => m.UserId == userId)
                .ToListAsync();

            return menuTypes;
        }
    }
}
