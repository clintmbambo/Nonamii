namespace Nonamii.Models.Inventory
{
    public class Extra
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<MenuItemExtra>? MenuItemExtras { get; set; }
    }
}
