namespace Restaurants.Applications.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}