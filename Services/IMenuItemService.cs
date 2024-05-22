namespace Nonamii.Services
{
    public interface IMenuItemService
    {
        string GetUserId();
        Task<IEnumerable<MenuItem>> GetMenuItemsAsync();
        Task<MenuItem> GetMenuItem(string name);
    }
}
