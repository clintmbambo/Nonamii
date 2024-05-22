namespace Nonamii.Repository
{
    public interface IMenuItemExtrasRepo
    {
        string? GetUserId();
        Task<IEnumerable<MenuItemExtra>> GetMenuItemExtrasAsync();
    }
}
