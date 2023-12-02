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
public class OrderServiceTests
{
    private readonly IOrderRepository _orderRepository;
    public OrderServiceTests()
    {
        _orderRepository = A.Fake<IOrderRepository>();
    }
    [Fact]
    public void OrderService_GetOrders_ReturnOrders()
    {
        // Arrange
        var fakeOrderRepository = A.Fake<IOrderRepository>();
        A.CallTo(() => fakeOrderRepository.GetOrders()).Returns(new List<Order> { new Order(), new Order() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var orderService = new OrderService(fakeOrderRepository, fakeDbContext);
        // Act
        var result = orderService.GetOrders();
        // Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public void OrderService_CreateOrder_ReturnOkObjectResult()
    {
        // Arrange
        var orderCreate = new OrderDto { Order_number = 1 };
        var order = new Order {Order_number = 1 };
        var fakeOrderRepository = A.Fake<IOrderRepository>();
        A.CallTo(() => fakeOrderRepository.GetOrders()).Returns(new List<Order> { new Order(), new Order() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var orderService = new OrderService(fakeOrderRepository, fakeDbContext);
        //Act
        var result = orderService.CreateOrder(order, orderCreate);
        //Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public void OrderService_UpdateOrder_ReturnOkObjectResult()
    {
        //Arrange
        var Order_number = 1;
        var updatedOrder = new OrderDto { Order_number = 2};
        var order = new Order { Order_number = 2};
        var fakeOrderRepository = A.Fake<IOrderRepository>();
        A.CallTo(() => fakeOrderRepository.GetOrders()).Returns(new List<Order> { new Order(), new Order() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var orderService = new OrderService(fakeOrderRepository, fakeDbContext);
        // Act
        var result = orderService.UpdateOrder(order, Order_number, updatedOrder);
        // Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public void OrderService_DeleteOrder_ReturnOkObjectResult()
    {
        // Arrange
        var Order_number = 1;
        var fakeOrderRepository = A.Fake<IOrderRepository>();
        A.CallTo(() => fakeOrderRepository.GetOrders()).Returns(new List<Order> { new Order(), new Order() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>();
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var orderService = new OrderService(fakeOrderRepository, fakeDbContext);
        // Act
        var result = orderService.DeleteOrder(Order_number);
        // Assert
        result.Should().NotBeNull();
    }
}