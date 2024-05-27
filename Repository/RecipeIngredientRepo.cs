namespace Nonamii.Repository
{
    public class RecipeIngredientRepo : IRecipeIngredientRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public RecipeIngredientRepo(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
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

        public async Task<IEnumerable<RecipeIngredient>> GetIngredientsAsync()
        {
            var recipeIngredients = await _db.RecipeIngredients
                .Include(m => m.Recipe)
                .Include(m => m.Ingredient)
                .Include(m => m.Measurement)
                .ToListAsync();

            return recipeIngredients;
        }

    }
}
