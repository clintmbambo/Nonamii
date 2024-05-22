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
    }
}
