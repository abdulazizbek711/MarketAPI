using MarketApi.Data;
using MarketApi.Interfaces;
using MarketApi.Models;
namespace MarketApi.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<User> GetUsers()
    {
        return _context.Users.OrderBy(u => u.User_ID).ToList();
    }
    public User GetUser(int User_ID)
    {
        return _context.Users.Where(u => u.User_ID==User_ID).FirstOrDefault();
    }
    public bool UserExists(int User_ID)
    {
        return _context.Users.Any(u => u.User_ID == User_ID);
    }
    public bool CreateUser(User user)
    {
        _context.Add(user);
        return Save();
    }
    public bool UpdateUser(User user)
    {
        _context.Update(user);
        return Save();
    }
    public bool DeleteUser(User user)
    {
        if (!_context.Users.Any(u =>u.User_ID == user.User_ID))
        {
            return false;
        }
        _context.Remove(user);
        return Save();
    }
    User IUserRepository.GetUser(string UserName)
    {
        return _context.Users.Where(u => u.UserName==UserName).FirstOrDefault();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}