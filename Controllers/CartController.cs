
namespace Nonamii.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepo _cartRepo;
        private readonly IUserManagement _userManagement;
        private readonly ApplicationDbContext _db;
        public CartController(ICartRepo cartRepo, IUserManagement userManagement, ApplicationDbContext db)
        {
            _cartRepo = cartRepo;
            _userManagement = userManagement;
            _db = db;
        }

        public async Task<IActionResult> AddItem(int menuItemId, int qty = 1, int redirect = 0)
        {
            var cartItemCount = await _cartRepo.AddItem(menuItemId, qty);
            if (redirect == 0)
            {
                return Ok(cartItemCount);
            }

            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int menuItemId)
        {
            var cartItemCount = _cartRepo.RemoveItem(menuItemId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart(CartViewModel cartVM)
        {
            var cart = await _cartRepo.GetUserCart();
            cartVM.CollectOrDeliver = cartVM.CollectOrDeliver;
            cartVM.SubTotal = cartVM.SubTotal;
            cartVM.Total = cartVM.Total;

            var cartView = new CartViewModel
            {
                Cart = cart,
                CollectOrDeliver = cartVM.CollectOrDeliver,
                SubTotal = cartVM.SubTotal,
                Total = cartVM.Total
            };

            ViewBag.Options = new List<string>()
            {
                "Select Option",
                "Delivery",
                "Self Collect"
            };


            if (cartView.CollectOrDeliver != "Delivery" && cartView.CollectOrDeliver != "Self Collect")
                return View(cartView);
            else if (cartView.CollectOrDeliver == "Self Collect")
                return RedirectToAction("CheckoutSession");
            else
                return RedirectToAction("CheckoutPage");
        }

        public async Task<IActionResult> CheckoutPage(CheckoutViewModel checkoutVm)
        {
            var card = await _userManagement.GetUserCard();
            var address = await _userManagement.GetUserAddress();
            IActionResult result = View();

            if(address != null)
            {
                var checkoutDetails = new CheckoutViewModel()
                {
                    Card = card,
                    Address = address,
                };

                result = View(checkoutDetails);
            }
            else
            {
                result = RedirectToAction("CheckoutPageEntry");
            }

            return result;
        }

        public async Task<IActionResult> CheckoutPageEntry()
        {
            return View();
        }

        public async Task<IActionResult> GetTotalItemsInCart()
        {
            var cartItemCount = await _cartRepo.GetCartItemCount();
            return Ok(cartItemCount);
        }

        public async Task<IActionResult> Checkout()
        {
            var isCheckedOut = await _cartRepo.Checkout();
            if(!isCheckedOut)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> CheckoutSession()
        {
            var cart = _cartRepo.GetUserCart();
            var domain = "https://localhost:7265/";

            var options = new SessionCreateOptions()
            {
                SuccessUrl = domain + $"/Cart/Checkout",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in cart.Result.Details)
            {
                var sessionListItems = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "zar",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.MenuItem.Name,
                        }
                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionListItems);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
