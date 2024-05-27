namespace Nonamii.Models.Inventory
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        //Get restaurant
        public string? UserId { get; set; }

        //Get Recipes
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        //Get Ingredients
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Required]
        [Display(Name = "Ingredient Portion")]
        public int MeasurementId { get; set; }
        public Measurement? Measurement { get; set; }
        public decimal amntValue { get; set; }

    }
}
