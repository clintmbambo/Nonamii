namespace Nonamii.Services
{
    public interface IUserOrdersRepo
    {
        string GetUserId();
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetPendingOrders();
        Task<IEnumerable<Order>> GetOrdersInProgress();
        Task<IEnumerable<Order>> GetOrdersReady();
        Task<IEnumerable<Order>> GetCancelledOrders();
        Task<IEnumerable<Order>> GetAllOrdersByUser();
        void CancelOrder(int id);
    }
}
