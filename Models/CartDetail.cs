using Nonamii.Models.Inventory;

namespace Nonamii.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        //Get user whose cart details it is.
        public string? UserId { get; set; }
        public int CartId { get; set; }
        public int MenuItemId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Cart? Cart { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
