using FakeItEasy;
using FluentAssertions;
using MarketApi.Data;
using MarketApi.Models;
using MarketApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace MarketApi.MarketApiTests.RepositoryTests;
public class OrderRepositoryTests
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
                databaseContext.Orders.Add(
                    new Order()
                    {
                        Order_number = i,
                        User_ID = 1,
                        Product_ID = 1,
                        Quantity = 3,
                        OrderQuantity_type = Order.OrderQuantityType.Pieces,
                        Price_Amount = 2,
                        Price_Currency = "$"
                    });
                await databaseContext.SaveChangesAsync();
            }
        }
        return databaseContext;
    }
    [Fact]
    public async  Task OrderRepository_GetOrders_ReturnsOrders()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var orderRepository = new OrderRepository(dbContext);
        //Act
        var result = orderRepository.GetOrders();
        //Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public async Task OrderRepository_GetOrder_ReturnOrderByOrder_number()
    {
        //Arrange
        int Order_number = 1;
        var dbContext = await GetDatabaseContext();
        var orderRepository = new OrderRepository(dbContext);
        //Act
        var result = orderRepository.GetOrder(Order_number);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Order>();
    }
    [Fact]
    public async Task OrderRepository_OrderExists_ReturnBool()
    {
        //Arrange
        var Order_number = 2;
        var dbContext = await GetDatabaseContext();
        var orderRepository = new OrderRepository(dbContext);
        //Act
        var result = orderRepository.OrderExists(Order_number);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task OrderRepository_CreateOrder_ReturnBool()
    {
        //Arrange
        var order = A.Fake<Order>();
        order.Price_Currency = "$";
        var dbContext = await GetDatabaseContext();
        var orderRepository = new OrderRepository(dbContext);
        //Act
        var result = orderRepository.CreateOrder(order);
        //Assert
        result.Should().BeTrue();
        dbContext.Orders.Should().Contain(order);
    }
    [Fact]
    public async Task OrderRepository_UpdateOrder_ReturnBool()
    {
        //Arrange
        var order = A.Fake<Order>();
        order.Price_Currency = "$";
        var dbContext = await GetDatabaseContext();
        var orderRepository = new OrderRepository(dbContext);
        //Act
        var result = orderRepository.UpdateOrder(order);
        //Assert
        result.Should().BeTrue();
        dbContext.Orders.Should().Contain(order);
    }
    [Fact]
    public async Task OrderRepository_DeleteOrder_ReturnBool()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var order = new Order();
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
        var orderRepository = new OrderRepository(dbContext);
        // Act
        var result = orderRepository.DeleteOrder(order);
        // Assert
        result.Should().BeTrue();
        dbContext.Orders.Should().NotContain(order);
    }
}