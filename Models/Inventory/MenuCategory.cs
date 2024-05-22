namespace Nonamii.Models.Inventory
{
    public class MenuCategory
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int CategoryId { get; set; }

        public Menu? Menu { get; set; }
        public Category? Category { get; set; }
    }
}
