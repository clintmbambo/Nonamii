namespace Nonamii.Services
{
    public interface IUserManagement
    {
        string GetUserId();
        Task<Models.UserDetails.Address> GetUserAddress();
        Task<Models.UserDetails.Card> GetUserCard();
    }
}