namespace Nonamii.Models.Inventory
{
    public class MenuType
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<Menu>? Menus { get; set; }
    }
}
