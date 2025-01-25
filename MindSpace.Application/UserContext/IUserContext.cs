namespace MindSpace.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}