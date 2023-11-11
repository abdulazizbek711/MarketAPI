using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;

namespace MarketApi.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public IEnumerable<User> GetUsers()
    {
        var users = _userRepository.GetUsers();
        if (users == null || !users.Any())
        {
            throw new InvalidOperationException("No products found");
        }
        return users;
    }

    public (bool, string) CreateUser(User user, UserDto userCreate)
    {
        if (userCreate == null)
        {
            return (false, "No Users Created");
        }
        var existingUser = _userRepository.GetUsers()
            .FirstOrDefault(c => c.UserName.Trim().ToUpper() == userCreate.UserName.Trim().ToUpper());

        if (existingUser != null)
        {
            return (false, "User already exists");
        }
        _userRepository.CreateUser(user);
        return (true, "User created successfully");
        
        
    }

    public bool UpdateUser(User user)
    {
        return _userRepository.UpdateUser(user);
    }
    

    public bool DeleteUser(User user)
    {
        return _userRepository.DeleteUser(user);
    }
}