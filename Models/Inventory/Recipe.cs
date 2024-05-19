namespace Nonamii.Models.Inventory
{
    public class Recipe
    {
        public int Id { get; set; }
        //Get restaurant
        public string? UserId { get; set; }
        //The MenuItem the the Recipe is for.
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        //Capture recipe information
        public string? Title { get; set; }
        public string? Instructions { get; set; }
        public decimal? Cost { get; set; } //Automatically calculated based on ingredient costs.

        public List<RecipeIngredient>? Ingredients { get; set;}
    }
}
