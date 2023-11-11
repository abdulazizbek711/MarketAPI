using AutoMapper;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;

namespace MarketApi.Helper;

public class UserMap: IUserMap

{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserMap(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    public User MapUser(UserDto userCreate)
    {
        var userMap = _mapper.Map<User>(userCreate);
        if (userMap == null)
        {
            throw new InvalidOperationException("Something went wrong while saving");
        }

        return userMap;
    }
}