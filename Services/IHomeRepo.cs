namespace Nonamii.Services
{
    public interface IHomeRepo
    {
        Task<List<MenuItem>> GetMenuItems(string searchTerm);
    }
}
