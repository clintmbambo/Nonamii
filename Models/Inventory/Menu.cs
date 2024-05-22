namespace Nonamii.Models.Inventory
{
    public class Menu
    {
        public int Id { get; set; }
        //Get restaurant this menu belongs to.
        public string? UserId { get; set; }
        //Get menu type
        public int TypeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public MenuType? Type { get; set; }
        public List<MenuCategory>? Categories { get; set; }

    }
}
