namespace Nonamii.Models.Inventory
{
    public class Category
    {
        public int Id { get; set; }

        public List<MenuCategory>? Menus { get; set; }

    }
}
