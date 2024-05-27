namespace Nonamii.Repository
{
    public interface IDeliveriesRepo
    {
        string? GetUserId();
        Task<IEnumerable<IdentityUser>> GetDriversAsync();
        Task<IEnumerable<IdentityUser>> GetRestaurantsAsync();
        Task<IEnumerable<IdentityUser>> GetCustomersAsync();
        Task<DeliveryVM> SendDeliveryRequest(int id);
        Task<DeliveryVM> FindDriver(int id);
    }
}
