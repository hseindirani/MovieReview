using MovieReview.Models;

namespace MovieReview.Interfaces
{
    public interface IUserRepository
    {
        User? GetUser(string username, string password);
    }
}
