namespace Nonamii.Repository
{
    public interface IRecipeRepo
    {
        string? GetUserId();
        Task<Recipe> GetRecipe(int Id);
        Task<IEnumerable<Recipe>> GetRecipesAsync();
    }
}
