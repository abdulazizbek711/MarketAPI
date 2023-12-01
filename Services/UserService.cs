using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
namespace MarketApi.Services;
public class UserService:IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly DataContext _context;

    public UserService(IUserRepository userRepository, DataContext context)
    {
        _userRepository = userRepository;
        _context = context;
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
            .FirstOrDefault(c => c.UserName != null &&
                                 c.UserName.Trim().ToUpper() == userCreate.UserName.Trim().ToUpper());
        if (existingUser != null)
        {
            return (false, "User already exists");
        }
        _userRepository.CreateUser(user);
        return (true, "User created successfully");
    }
    public (bool, string) UpdateUser(User user, int User_ID,  UserDto updatedUser)
    {
        if (updatedUser == null || User_ID != updatedUser.User_ID)
        {
            return (false, "No users updated");
        }
        var existingUser = _userRepository.GetUser(User_ID);
        if (existingUser == null)
        {
            return (false, "No users found");
        }
        _context.Entry(existingUser).State = EntityState.Detached;
        existingUser.UserName = updatedUser.UserName ?? existingUser.UserName;
        existingUser.Email = updatedUser.Email ?? existingUser.Email;
        existingUser.PhoneNumber = updatedUser.PhoneNumber ?? existingUser.PhoneNumber;
        // Update the user in the repository
        _userRepository.UpdateUser(existingUser);
        return (true, "User updated successfully");
    }
    public (bool, string) DeleteUser(int User_ID)
    {
        if (!_userRepository.UserExists(User_ID))
        {
            return (false, "User not found");
        }
        var userToDelete = _userRepository.GetUser(User_ID);
        if (userToDelete == null)
        {
            return (false, "User not exist");
        }
        _userRepository.DeleteUser(userToDelete);
        return (true, "User successfully deleted");
    }
}