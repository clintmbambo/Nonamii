
namespace Nonamii.Repository
{
    public class IngredientRepo : IIngredientRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public IngredientRepo(UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor, ApplicationDbContext db)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _db = db;
        }

        public string? GetUserId()
        {
            var principal = _contextAccessor.HttpContext.User;
            string? userId = _userManager.GetUserId(principal);

            return userId;
        }
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            var ingredients = await _db.Ingredients
                .Include(m => m.Recipes)
                .Where(u => u.UserId == GetUserId())
                .ToListAsync();

            return ingredients;
        }
    }
}
