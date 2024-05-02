namespace Nonamii.Models.Inventory
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Amount { get; set; }
        public decimal Cost { get;set; }

        public List<MenuItemIngredient>? MenuItems{ get; set; }
    }
}
