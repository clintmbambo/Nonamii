namespace Nonamii.Models.Inventory
{
    public class Size
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<MenuItemSize>? MenuItems { get; set; }
    }
}
