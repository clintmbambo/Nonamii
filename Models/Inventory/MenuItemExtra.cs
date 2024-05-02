namespace Nonamii.Models.Inventory
{
    public class MenuItemExtra
    {
        public int Id { get; set; }
        public int ExtraId { get; set; }
        public int MenuItemId { get; set; }
        public bool IsActive { get; set; } = true;

        public Extra? Extra { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
