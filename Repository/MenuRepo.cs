namespace Nonamii.Repository
{
    public class MenuRepo : IMenuRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public MenuRepo(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
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

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            string? userId = GetUserId();

            var menus = await _db.Menu
                .Include(x => x.Type)
                .Include(m => m.Categories)
                .Where(m => m.UserId == userId)
                .ToListAsync();

            return menus;
        }
    }
}
