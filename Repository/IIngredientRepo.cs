namespace Nonamii.Repository
{
    public interface IIngredientRepo
    {
        string? GetUserId();
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
    }
}
