namespace Nonamii.Repository
{
    public interface IDeliveriesRepo
    {
        string? GetUserId();
        Task<IEnumerable<IdentityUser>> GetDriversAsync();
        Task<IEnumerable<IdentityUser>> GetRestaurantsAsync();
        Task<IdentityUser> GetRestaurantAsync(int orderId);
        Task<IEnumerable<IdentityUser>> GetCustomersAsync();
        Task<IdentityUser> GetCustomerAsync(int orderId);
        void CreateDeliveryAsync(int orderId);

        Task<DeliveryVM> SendDeliveryRequest(int id);
        Task<DeliveryVM> FindDriver(int id);
    }
}
