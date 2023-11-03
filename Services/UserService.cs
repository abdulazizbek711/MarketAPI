using AutoMapper;
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
        return _userRepository.GetUsers();
    }
    public bool CreateUser(User user)
    {
        return _userRepository.CreateUser(user);
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