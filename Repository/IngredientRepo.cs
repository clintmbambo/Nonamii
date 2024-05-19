
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
        public async Task<IEnumerable<Ingredient>> GetIngredients()
        {
            var ingredients = await (from ingredient in _db.Ingredients
                                     join recipeIngredient in _db.RecipeIngredients
                                     on ingredient.Id equals recipeIngredient.IngredientId

                                     join recipe in _db.Recipes
                                     on recipeIngredient.RecipeId equals recipe.Id
                                     select ingredient).ToListAsync();

            return ingredients;
        }
    }
}
