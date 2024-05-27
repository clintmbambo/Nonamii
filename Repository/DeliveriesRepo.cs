
namespace Nonamii.Repository
{
    public class DeliveriesRepo : IDeliveriesRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public DeliveriesRepo(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public string GetUserId()
        {
            var principal = _contextAccessor.HttpContext.User;
            string? userId = _userManager.GetUserId(principal);

            return userId;
        }
        public async Task<IEnumerable<IdentityUser>> GetDriversAsync()
        {
            //There's only one role.
            var role = await _db.Roles
                .FirstOrDefaultAsync(m => m.Name == "Driver");

            if(role == null)
            {
                throw new Exception("No drivers found");
            }
            
            var driverRoleUserIds = await _db.UserRoles
            .Where(m => m.RoleId == role.Id).ToListAsync();


            var drivers = new List<IdentityUser>();
            foreach(var driverUserRole in driverRoleUserIds)
            {
                var user = await _db.Users
                    .FirstOrDefaultAsync(m => m.Id == driverUserRole.UserId);

                if(user is not null)
                {
                    drivers.Add(user);
                }
            }
            return drivers;
        }
        public async Task<IEnumerable<IdentityUser>> GetRestaurantsAsync()
        {
            //There's only one role.
            var role = await _db.Roles
                .FirstOrDefaultAsync(m => m.Name == "Restaurant");

            if (role == null)
            {
                throw new Exception("No restaurants found");
            }

            var restaurantRoleUserIds = await _db.UserRoles
            .Where(m => m.RoleId == role.Id).ToListAsync();


            var restaurants = new List<IdentityUser>();
            foreach (var restaurantUserRole in restaurantRoleUserIds)
            {
                var user = await _db.Users
                    .FirstOrDefaultAsync(m => m.Id == restaurantUserRole.UserId);

                if (user is not null)
                {
                    restaurants.Add(user);
                }
            }
            return restaurants;
        }
        public async Task<IEnumerable<IdentityUser>> GetCustomersAsync()
        {
            //There's only one role.
            var role = await _db.Roles
                .FirstOrDefaultAsync(m => m.Name == "User");

            if (role == null)
            {
                throw new Exception("No customers found");
            }

            var customerRoleUserIds = await _db.UserRoles
            .Where(m => m.RoleId == role.Id).ToListAsync();


            var customers = new List<IdentityUser>();
            foreach (var customerUserRole in customerRoleUserIds)
            {
                var user = await _db.Users
                    .FirstOrDefaultAsync(m => m.Id == customerUserRole.UserId);

                if (user is not null)
                {
                    customers.Add(user);
                }
            }
            return customers;
        }
        public async Task<DeliveryVM> SendDeliveryRequest(int id)
        {
            if(id <= 0)
            {
                throw new Exception("Order does not exist");
            }

            var order = await _db.Orders
                .Include(m => m.OrderDetails)
                .ThenInclude(m => m.MenuItem)
                .Include(m => m.OrderStatus)
                .FirstOrDefaultAsync(m => m.Id == id);

            string? restaurantId = "";
            while(string.IsNullOrEmpty(restaurantId) && order.OrderDetails.Count > 0)
            {
                foreach(var item in  order.OrderDetails)
                {
                    restaurantId = item.MenuItem.UserId;
                }
            }

            if (string.IsNullOrEmpty(restaurantId))
            {
                throw new Exception("Invalid restaurant id");
            }

            var customer = await _db.Users
                .FirstOrDefaultAsync(m => m.Id == order.UserId);

            var restaurant = await _db.Users
                .FirstOrDefaultAsync(m => m.Id == restaurantId);

            var delivery = new DeliveryVM
            {
                OrderId = id,
                CustomerId = customer.Id,
                CustomerName = customer.UserName,
                CustomerAddress = customer.PhoneNumber,
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.UserName,
                RestaurantAddress = restaurant.PhoneNumber,
                PackageWeight = 2
            };

            return delivery;
        }
        public async Task<DeliveryVM> FindDriver(int id)
        {
            var deliveryDetails = await SendDeliveryRequest(id);
            return deliveryDetails;
        }
    }
}
