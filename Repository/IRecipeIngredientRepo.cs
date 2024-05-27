namespace Nonamii.Repository
{
    public interface IRecipeIngredientRepo
    {
        string? GetUserId();
        Task<IEnumerable<RecipeIngredient>> GetIngredientsAsync();
    }
}
