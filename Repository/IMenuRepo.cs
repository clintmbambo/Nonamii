namespace Nonamii.Repository
{
    public interface IMenuRepo
    {
        string? GetUserId();
        Task<IEnumerable<Menu>> GetAllMenusAsync();
    }
}
