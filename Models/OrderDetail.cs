using Nonamii.Models.Inventory;

namespace Nonamii.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public int MenuItemId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Order? Order { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
