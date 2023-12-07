using FakeItEasy;
using FluentAssertions;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using MarketApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace MarketApi.MarketApiTests.ServiceTests;
public class UserServiceTests
{
    private readonly IUserRepository _userRepository;
    public UserServiceTests()
    {
        _userRepository = A.Fake<IUserRepository>();
    }
    [Fact]
    public void UserService_GetUsers_ReturnUsers()
    {
        // Arrange
        var fakeUserRepository = A.Fake<IUserRepository>();
        A.CallTo(() => fakeUserRepository.GetUsers()).Returns(new List<User> { new User(), new User() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var userService = new UserService(fakeUserRepository, fakeDbContext);
        // Act
        var result = userService.GetUsers();
        // Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public void UserService_CreateUser_ReturnOkObjectResult()
    {
        // Arrange
        var userCreate = new UserDto { UserName = "Abdulaziz" };
        var user = new User { UserName = "Abdulaziz" };
        var fakeUserRepository = A.Fake<IUserRepository>();
        A.CallTo(() => fakeUserRepository.GetUsers()).Returns(new List<User>());
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var userService = new UserService(fakeUserRepository, fakeDbContext);
        //Act
        var result = userService.CreateUser(user, userCreate);
        //Assert
        result.Should().NotBeNull();
        result.Item1.Should().BeTrue();
        result.Item2.Should().Be("User created successfully");
    }
    [Fact]
    public void UserService_UpdateUser_ReturnOkObjectResult()
    {
        //Arrange
        var User_ID = 1;
        var updatedUser = new UserDto { User_ID = 1};
        var user = new User { User_ID = 1};
        var fakeUserRepository = A.Fake<IUserRepository>();
        A.CallTo(() => fakeUserRepository.GetUsers()).Returns(new List<User> { new User(), new User() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var userService = new UserService(fakeUserRepository, fakeDbContext);
        // Act
        var result = userService.UpdateUser(user, User_ID, updatedUser);
        // Assert
        result.Should().NotBeNull();
        result.Item1.Should().BeTrue();
        result.Item2.Should().Be("User updated successfully");
    }
    [Fact]
    public void UserService_DeleteUser_ReturnOkObjectResult()
    {
        // Arrange
        var User_ID = 40;
        var fakeUserRepository = A.Fake<IUserRepository>();
        A.CallTo(() => fakeUserRepository.UserExists(User_ID)).Returns(true);
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var userService = new UserService(fakeUserRepository, fakeDbContext);
        // Act
        var result = userService.DeleteUser(User_ID);
        // Assert
        result.Should().NotBeNull();
    }
}