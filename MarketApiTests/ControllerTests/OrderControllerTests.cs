using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MarketApi.Controllers;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace MarketApi.MarketApiTests.ControllerTests;
public class OrderControllerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderService _orderService;
    private readonly IOrderMap _orderMap;
    private readonly IMapper _mapper;
    public OrderControllerTests()
    {
        _orderRepository = A.Fake<IOrderRepository>();
        _userRepository = A.Fake<IUserRepository>();
        _productRepository = A.Fake<IProductRepository>();
        _orderService = A.Fake<IOrderService>();
        _orderMap = A.Fake<IOrderMap>();
        _mapper = A.Fake<IMapper>();
    }
    [Fact]
    public void OrderController_GetOrders_ReturnsOkObjectResult()
    {
        //Arrange
        var orders = A.CollectionOfDummy<OrderDto>(3); 
        var orderList = A.CollectionOfDummy<OrderDto>(3).ToList(); 
        A.CallTo(() => _mapper.Map<List<OrderDto>>(orders)).Returns(orderList);
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.GetOrders();
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void OrderController_CreateUser_ReturnsOkObjectResult(int User_ID)
    {
        //Arrange
        var order = A.Fake<OrderDto>();
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.CreateOrder(User_ID, order);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
    }
    [Fact]
    public void OrderController_UpdateOrder_ReturnsOkObjectResult()
    {
        //Arrange
        var Order_ID = 1;
        var order = A.Fake<OrderDto>();
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.UpdateOrder(Order_ID, order);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void OrderController_DeleteOrder_ReturnsOkObjectResult(int Order_ID)
    {
        //Arrange
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.DeleteOrder(Order_ID);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
    }
}