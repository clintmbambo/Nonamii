namespace Nonamii.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext? _db;

        public UserManagement(ApplicationDbContext? db, IHttpContextAccessor? httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string? userId = _userManager.GetUserId(principal);

            return userId;
        }

        public async Task<Models.UserDetails.Address> GetUserAddress()
        {
            var userId = GetUserId();
            var userAddress = await (from address in _db.Address
                                     where address.UserId == userId
                                     select address).FirstOrDefaultAsync();

            return userAddress;
        }

        public async Task<Models.UserDetails.Card> GetUserCard()
        {
            var userId = GetUserId();
            var userCard = await (from card in _db.Card
                                  where card.UserId == userId
                                  select card).FirstOrDefaultAsync();

            return userCard;
        }
    }
}
