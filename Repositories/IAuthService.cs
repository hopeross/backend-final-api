using social_api.Models;

namespace social_api.Repositories;

public interface IAuthService
{
    User CreateUser(User user);
    string SignIn(string email, string password);
    User UpdateUser(User user);

    User GetUserById(int userId);
}