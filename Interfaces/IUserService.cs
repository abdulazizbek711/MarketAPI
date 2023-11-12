using MarketApi.Dtos;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace MarketApi.Interfaces;

public interface IUserService
{
    IEnumerable<User> GetUsers();
    public (bool, string) CreateUser(User user, UserDto userCreate);
    public (bool, string) UpdateUser(User user, int User_ID, UserDto updatedUser);
    public (bool, string) DeleteUser(int User_ID);
}