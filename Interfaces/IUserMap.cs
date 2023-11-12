using MarketApi.Dtos;
using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IUserMap
{
    public User MapUser(UserDto userCreate);
    public User MappUser(int User_ID, UserDto updatedUser);
}