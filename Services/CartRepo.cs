
using Nonamii.Controllers;

namespace Nonamii.Services
{
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public CartRepo(ApplicationDbContext db, IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
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

        public async Task<Cart> GetCart(string userId)
        {
            var cart = _db.Carts.FirstOrDefault(x => x.UserId == userId);
            return cart;
        }
        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var data = await (from cart in _db.Carts
                              join details in _db.CartsDetails
                              on cart.Id equals details.CartId
                              where cart.UserId == userId
                              select new { details.Id }).ToListAsync();
            return data.Count;
        }

        public async Task<Cart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new Exception("Invalid user");
            }

            //Joining all the tables that make up cart details.
            var cart = await _db.Carts
                .Include(x => x.Details)
                .ThenInclude(x => x.MenuItem)
                .ThenInclude(x => x.ItemSizes)
                .Where(x => x.UserId == userId).FirstOrDefaultAsync();

            return cart;
        }

        public async Task<int> AddItem(int menuItemId, int qty)
        {
            string user = GetUserId();
            var executionStrategy = _db.Database.CreateExecutionStrategy();

            await executionStrategy.Execute(async () =>
            {
                using(var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(user))
                        {
                            throw new Exception("User is not logged-in");
                        }

                        var cart = await GetCart(user);
                        if (cart is null)
                        {
                            cart = new Cart
                            {
                                UserId = user,
                                IsActive = true,
                            };
                            _db.Carts.Add(cart);
                        }
                        await _db.SaveChangesAsync();

                        //Cart exists or has been created. We now add details of the cart (add item to the cart).
                        var menuItem = _db.MenuItems.Find(menuItemId);
                        var cartItem = _db.CartsDetails.FirstOrDefault(a => a.CartId == cart.Id && a.MenuItemId == menuItemId);
                        if (cartItem is not null)
                        {
                            cartItem.Quantity += qty;
                        }
                        else
                        {
                            cartItem = new CartDetail
                            {
                                MenuItemId = menuItemId,
                                CartId = cart.Id,
                                Quantity = qty,
                                Price = menuItem.Price
                            };
                            _db.CartsDetails.Add(cartItem);
                        }
                        await _db.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }

                }
            });
            
            var cartItemCount = await GetCartItemCount(user);
            return cartItemCount;
        }

        public async Task<int> RemoveItem(int menuItemId)
        {
            var userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged in!");
                }

                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new Exception("User doesn't have a cart!");
                }
                _db.SaveChanges();

                //Remove cart item from cart, if cart exists.
                var cartItem = _db.CartsDetails.FirstOrDefault(x => x.CartId == cart.Id && x.MenuItemId == menuItemId);
                if (cartItem is null)
                    throw new Exception("Cart is empty!");
                else if (cartItem.Quantity == 1)
                {
                    _db.CartsDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity -= 1;
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to remove item {ex.Message}");
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<bool> Checkout()
        {
            var userId = GetUserId();
            var executionStrategy = _db.Database.CreateExecutionStrategy();
            bool returnValue = true;

            //if (string.IsNullOrEmpty(userId))
            //    throw new Exception("User is not logged in");

            //var cart = await GetCart(userId);
            //if (cart is null)
            //{
            //    throw new Exception("User does not have a cart");
            //}

            //var cartDetails = await _db.CartsDetails.Where(a => a.CartId == cart.Id).ToListAsync();
            //if (cartDetails == null)
            //{
            //    throw new Exception("Null");
            //}

            //var order = new Order
            //{
            //    UserId = userId,
            //    DateCreated = DateTime.Now,
            //    IsActive = true,
            //    OrderStatusId = 1
            //};
            //await _db.AddAsync(order);
            //await _db.SaveChangesAsync();

            //foreach (var item in cartDetails)
            //{
            //    var orderDetail = new OrderDetail
            //    {
            //        OrderId = order.Id,
            //        MenuItemId = item.MenuItemId,
            //        Price = item.Price,
            //        Quantity = item.Quantity,
            //    };
            //    await _db.OrdersDetails.AddAsync(orderDetail);
            //}
            //await _db.SaveChangesAsync();

            //_db.CartsDetails.RemoveRange(cartDetails);
            //await _db.SaveChangesAsync();

            //returnValue = true;

            //return returnValue;
            await executionStrategy.Execute(async () =>
            {
                using (var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(userId))
                            throw new Exception("User is not logged in");

                        var cart = await GetCart(userId);
                        if (cart is null)
                        {
                            throw new Exception("User does not have a cart");
                        }

                        var cartDetails = await _db.CartsDetails.Where(a => a.CartId == cart.Id).ToListAsync();
                        if (cartDetails == null)
                        {
                            throw new Exception("Null");
                        }

                        var order = new Order
                        {
                            UserId = userId,
                            DateCreated = DateTime.Now,
                            IsActive = true,
                            OrderStatusId = 1
                        };
                        await _db.AddAsync(order);
                        await _db.SaveChangesAsync();

                        foreach (var item in cartDetails)
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderId = order.Id,
                                MenuItemId = item.MenuItemId,
                                Price = item.Price,
                                Quantity = item.Quantity,
                            };
                            await _db.OrdersDetails.AddAsync(orderDetail);
                        }
                        await _db.SaveChangesAsync();

                        _db.CartsDetails.RemoveRange(cartDetails);
                        await _db.SaveChangesAsync();
                        await transaction.CommitAsync();

                        returnValue = true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        returnValue = false;
                    }
                }
            });
            return returnValue;
        }
    }
}
