namespace Nonamii.Models.Inventory
{
    public class MenuItemIngredient
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int IngredientId { get; set; }

        public Ingredient? Ingredient { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
