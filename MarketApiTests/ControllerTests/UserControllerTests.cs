using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MarketApi.Controllers;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace MarketApi.MarketApiTests.ControllerTests;
public class UserControllerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IUserMap _userMap;
    private readonly IMapper _mapper;
    public UserControllerTests()
    {
        _userRepository = A.Fake<IUserRepository>();
        _userService = A.Fake<IUserService>();
        _userMap = A.Fake<IUserMap>();
        _mapper = A.Fake<IMapper>();
    }
    [Fact]
    public void UserController_GetUsers_ReturnsOkObjectResult()
    {
        //Arrange
        var users = A.CollectionOfDummy<UserDto>(3); 
        var userList = A.CollectionOfDummy<UserDto>(3).ToList(); 
        A.CallTo(() => _mapper.Map<List<UserDto>>(users)).Returns(userList);
        var controller = new UserController(_userRepository, _mapper, _userService, _userMap);
        //Act
        var result = controller.GetUsers();
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
    }
    [Fact]
    public void UserController_CreateUser_ReturnsOkObjectResult()
    {
        //Arrange
        var user = A.Fake<UserDto>();
        var controller = new UserController(_userRepository, _mapper, _userService, _userMap);
        //Act
        var result = controller.CreateUser(user);
        //Assert
        result.Should().NotBeNull();
        if (result is NoContentResult noContentResult)
        {
            noContentResult.StatusCode.Should().Be(204); 
        }
        else if (result is BadRequestObjectResult badRequest)
        {
            badRequest.StatusCode.Should().Be(400);
        }
    }

    [Fact]
    public void UserController_UpdateUser_ReturnsOkObjectResult()
    {
        //Arrange
        var User_ID = 1;
        var user = A.Fake<UserDto>();
        var controller = new UserController(_userRepository, _mapper, _userService, _userMap);
        //Act
        var result = controller.UpdateUser(User_ID, user);
        //Assert
        result.Should().NotBeNull();
        if (result is NoContentResult noContentResult)
        {
            noContentResult.StatusCode.Should().Be(204); 
        }
        else if (result is BadRequestObjectResult badRequest)
        {
            badRequest.StatusCode.Should().Be(400);
        }
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void UserController_DeleteProduct_ReturnsOkObjectResult(int User_ID)
    {
        //Arrange
        var controller = new UserController(_userRepository, _mapper, _userService, _userMap);
        //Act
        var result = controller.DeleteUser(User_ID);
        //Assert
        result.Should().NotBeNull();
        if (result is NoContentResult noContentResult)
        {
            noContentResult.StatusCode.Should().Be(204); 
        }
        else if (result is BadRequestObjectResult badRequest)
        {
            badRequest.StatusCode.Should().Be(400);
        }
    }
}