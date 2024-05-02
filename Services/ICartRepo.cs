namespace Nonamii.Services
{
    public interface ICartRepo
    {
        string GetUserId();
        Task<Cart> GetCart(string userId);
        Task<Cart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<int> AddItem(int menuItemId, int qty);
        Task<int> RemoveItem(int menuItemId);
        Task<bool> Checkout();
    }
}
