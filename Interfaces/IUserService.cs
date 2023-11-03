using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IUserService
{
    IEnumerable<User> GetUsers();
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(User user);
}