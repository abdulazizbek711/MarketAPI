using MarketApi.Dtos;
using MarketApi.Models;

namespace MarketApi.Interfaces;

public interface IUserMap
{
    public User MapUser(UserDto userCreate);
}