namespace Nonamii.Models.Inventory
{
    public class Ingredient
    {
        public int Id { get; set; }
        //identify the restaurant the ingredient belongs to.
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal? UnitOfMeasureValue { get; set; }
        public decimal CostPerUnit { get;set; }

        public List<RecipeIngredient>? Recipes { get; set; }
    }
}
