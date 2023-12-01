using FakeItEasy;
using FluentAssertions;
using MarketApi.Data;
using MarketApi.Models;
using MarketApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace MarketApi.MarketApiTests.RepositoryTests;
public class UserRepositoryTests
{
    private async Task<DataContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new DataContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Users.CountAsync() <= 0)
        {
            for (int i = 1; i <= 10; i++)
            {
                databaseContext.Users.Add(
                    new User()
                    {
                        UserName = "Abdulaziz",
                        User_ID = i,
                        Email = "panjiyevabdulaziz77@gmail.com",
                        PhoneNumber = 977119717
                    });
                await databaseContext.SaveChangesAsync();
            }
        }
        return databaseContext;
    }
    [Fact]
    public async  Task UserRepository_GetUsers_ReturnsProducts()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext);
        //Act
        var result = userRepository.GetUsers();
        //Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public async Task UserRepository_GetUser_ReturnUserByUser_ID()
    {
        //Assign
        int User_ID = 2;
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext);
        //Act
        var result = userRepository.GetUser(User_ID);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
    }
    [Fact]
    public async Task UserRepository_UserExists_ReturnBool()
    {
        //Assign
        var User_ID = 2;
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext);
        //Act
        var result = userRepository.UserExists(User_ID);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task UserRepository_CreateUser_ReturnBool()
    {
        //Assign
        var user = A.Fake<User>();
        user.Email = "panjiyevs@gmail.com"; // Set required properties
        user.UserName = "insanely"; // Set required properties*/
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext);
        //Act
        var result = userRepository.CreateUser(user);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task UserRepository_UpdateUser_ReturnBool()
    {
        //Assign
        var user = A.Fake<User>();
        user.Email = "panjiyevs@gmail.com"; // Set required properties
        user.UserName = "insanely"; // Set required properties*/
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext);
        //Act
        var result = userRepository.UpdateUser(user);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task UserRepository_DeleteUser_ReturnBool()
    {
        //Assign
        var user = A.Fake<User>();
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext);
        //Act
        var result = userRepository.DeleteUser(user);
        //Assert
        result.Should().BeFalse();
    }
}