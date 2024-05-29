
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

        public async Task<IdentityUser> GetRestaurantAsync(int orderId)
        {
            if(orderId <= 0)
            {
                throw new Exception("Invalid order");
            }

            var orderDetails = await _db.OrdersDetails
                .FirstOrDefaultAsync(m => m.OrderId == orderId);

            var menuItem = await _db.MenuItems
                .FirstOrDefaultAsync(m => m.Id == orderDetails.MenuItemId);

            var restaurant = await _db.Users
                .FindAsync(menuItem.UserId);

            return restaurant;
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
        
        public async Task<IdentityUser> GetCustomerAsync(int orderId)
        {
            if (orderId <= 0)
                throw new Exception("Invalid order");

            var order = await _db.Orders
                .FindAsync(orderId);

            var customer = await _db.Users
                .FindAsync(order.UserId);

            return customer;
        }

        public async void CreateDeliveryAsync(int orderId)
        {
            if(orderId <= 0)
            {
                throw new Exception("Invalid order");
            }

            var customer = await GetCustomerAsync(orderId);
            var customerDetails = await _db.Address
                .FirstOrDefaultAsync(m => m.UserId == customer.Id);

            var restaurant = await GetRestaurantAsync(orderId);
            var restaurantDetails = await _db.Restaurant
                .FirstOrDefaultAsync(m => m.UserId == restaurant.Id);

            var delivery = new Delivery()
            {
                Key = orderId,
                CustomerAddress = customerDetails.Street,
                RestaurantId = restaurant.Id,
                RestaurantName = restaurantDetails.Name,
                RestaurantAddress = restaurantDetails.Address,
                PackageWeight = 5
            };

            await _db.AddAsync(delivery);
            await _db.SaveChangesAsync();
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

            var customer = await _db.Users
                .FirstOrDefaultAsync(m => m.Id == order.UserId);

            var restaurant = await _db.Users
                .FirstOrDefaultAsync(m => m.Id == order.RestaurantId);

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
