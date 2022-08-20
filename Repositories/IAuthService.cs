using social_api.Models;

namespace social_api.Repositories;

public interface IAuthService
{
    User CreateUser(User user);
    string SignIn(string email, string password);
    User UpdateUser(User editUser);

    User GetUserById(int userId);
}