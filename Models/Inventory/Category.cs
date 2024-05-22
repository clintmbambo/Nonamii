namespace Nonamii.Models.Inventory
{
    public class Category
    {
        public int Id { get; set; }
        //Get restaurant
        public string? UserId { get; set; }

        public List<MenuCategory>? Menus { get; set; }

    }
}
