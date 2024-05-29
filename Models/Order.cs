namespace Nonamii.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? RestaurantId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public OrderStatus? OrderStatus { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
