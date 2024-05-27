
namespace Nonamii.Repository
{
    public class RecipeRepo : IRecipeRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public RecipeRepo(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
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

        public async Task<Recipe> GetRecipe(int Id)
        {
            var recipe = await _db.Recipes.Where(x => x.Id == Id).FirstOrDefaultAsync();
            
            if (recipe == null)
            {
                throw new Exception("No recipe matching this id found");
            }

            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            var recipes = await _db.Recipes
                .Include(x => x.Ingredients)
                .Include(m => m.MenuItem)
                .Where(x => x.UserId == GetUserId())
                .ToListAsync();
            return recipes;
        }
    }
}
