using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IUserRepository
{
    ICollection<User> GetUsers();
    User GetUser(int User_ID);
    User GetUser(string UserName);
    bool UserExists(int User_ID);
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(User user);
}