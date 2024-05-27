namespace Nonamii.Services
{
    public class UserOrdersRepo : IUserOrdersRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserOrdersRepo(ApplicationDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string? userId = _userManager.GetUserId(principal);
            return userId;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Invalid User");

            var orders = await _db.Orders
                .Include(x => x.OrderStatus)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.MenuItem)
                .ThenInclude(x => x.ItemSizes)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return orders;
        }
        public async Task<IEnumerable<Order>> GetPendingOrders()
        {
            var orders = await _db.Orders
                .Include(x => x.OrderStatus)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.MenuItem)
                .ThenInclude(x => x.ItemSizes)
                .Where(a => a.OrderStatusId == 1)
                .ToListAsync();

            return orders;

        }
        public async Task<IEnumerable<Order>> GetAllOrdersByUser()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Invalid User");

            var orders = await _db.Orders
                .Include(x => x.OrderStatus)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.MenuItem)
                .ThenInclude(x => x.ItemSizes)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return orders;
        }
        public async Task<IEnumerable<Order>> GetOrdersInProgress()
        {

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Invalid User");

            var orders = await _db.Orders
                .Include(x => x.OrderStatus)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.MenuItem)
                .ThenInclude(x => x.ItemSizes)
                .Where(a => a.UserId == userId && a.OrderStatusId == 2)
                .ToListAsync();

            return orders;
        }
        public async Task<IEnumerable<Order>> GetOrdersReady()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Invalid User");

            var orders = await _db.Orders
                .Include(x => x.OrderStatus)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.MenuItem)
                .ThenInclude(x => x.ItemSizes)
                .Where(a => a.UserId == userId && a.OrderStatusId == 3)
                .ToListAsync();

            return orders;
        }
        public async Task<IEnumerable<Order>> GetCancelledOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Invalid User");

            var orders = await _db.Orders
                .Include(x => x.OrderStatus)
                .Include(a => a.OrderDetails)
                .ThenInclude(a => a.MenuItem)
                .ThenInclude(x => x.ItemSizes)
                .Where(a => a.OrderStatusId == 5)
                .ToListAsync();

            return orders;
        }
        public void CancelOrder(int id)
        {
            //Verify User
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Invalid User");

            //Find order in database and create new order
            var order = _db.Orders.Find(id);

            order.OrderStatusId = 5;
            _db.Update(order);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<RecipeIngredient>> GetRecipeIngredientsAsync(List<MenuItem> list)
        {
            var ingredients = new List<RecipeIngredient>();
            foreach(var menuItem in list)
            {
                var recipes = await _db.Recipes
                    .Include(m => m.MenuItem)
                    .Where(m => m.MenuItemId == menuItem.Id)
                    .ToListAsync();

                foreach(var recipe in recipes)
                {
                    var recipeIngredientsList = await _db.RecipeIngredients
                        .Include(m => m.Recipe)
                        .Where(m => m.RecipeId == recipe.Id)
                        .ToListAsync();

                    foreach(var item in recipeIngredientsList)
                    {
                        ingredients.Add(item);
                    }
                }
            }

            return ingredients;
        }

        public async Task<IEnumerable<Ingredient>> UpdateIngredientsAsync(List<RecipeIngredient> list)
        {
            var ingredientList = new List<Ingredient>();
            foreach(var recipeIngredient in list)
            {
                foreach(var ingredient in _db.Ingredients)
                {
                    if(ingredient.Id == recipeIngredient.IngredientId)
                        ingredientList.Add(ingredient);
                }
            }

            return ingredientList;
        }
    }
}
