namespace Nonamii.Models.Inventory
{
    public class MenuItemSize
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int SizeId { get; set; }
        public int MenuItemId { get; set; }
        public decimal Price { get; set; }

        public Size? Size { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
