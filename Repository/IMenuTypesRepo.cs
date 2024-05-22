namespace Nonamii.Repository
{
    public interface IMenuTypesRepo
    {
        string? GetUserId();
        Task<IEnumerable<MenuType>> GetMenuTypesAsync();
    }
}
