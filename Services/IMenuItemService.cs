namespace Nonamii.Services
{
    public interface IMenuItemService
    {
        string GetUserId();
        Task<IList<MenuItem>> GetMenuItems();
        Task<MenuItem> GetMenuItem(string name);
    }
}
