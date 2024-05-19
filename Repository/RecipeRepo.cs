
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
    }
}
