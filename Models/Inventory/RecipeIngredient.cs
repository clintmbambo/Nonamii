namespace Nonamii.Models.Inventory
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        //Get restaurant
        public string? UserId { get; set; }
        //Get Recipes
        public int RecipeId { get; set; }
        //Get Ingredients
        public int IngredientId { get; set; }
    }
}
