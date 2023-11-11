using MarketApi.Dtos;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Interfaces;
public interface IUserService
{
    IEnumerable<User> GetUsers();
    public (bool, string) CreateUser(User user, UserDto userCreate);
    bool UpdateUser(User user);
    bool DeleteUser(User user);
}