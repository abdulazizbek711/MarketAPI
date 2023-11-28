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
        //Assign
        var orders = A.CollectionOfDummy<OrderDto>(3); // Use A.CollectionOfDummy to create a collection of fake ProductDto
        var orderList = A.CollectionOfDummy<OrderDto>(3).ToList(); 
        A.CallTo(() => _mapper.Map<List<OrderDto>>(orders)).Returns(orderList);
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.GetOrders();
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void OrderController_CreateUser_ReturnsOkObjectResult(int User_ID)
    {
        //Assign
        var order = A.Fake<OrderDto>();
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.CreateOrder(User_ID, order);
        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void OrderController_UpdateOrder_ReturnsOkObjectResult()
    {
        //Assign
        var Order_ID = 1;
        var order = A.Fake<OrderDto>();
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.UpdateOrder(Order_ID, order);
        //Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void OrderController_DeleteOrder_ReturnsOkObjectResult(int Order_ID)
    {
        //Assign
        var controller = new OrderController(_mapper, _orderService, _orderRepository, _userRepository, _orderMap, _productRepository);
        //Act
        var result = controller.DeleteOrder(Order_ID);
        //Assert
        result.Should().BeOfType(typeof(ObjectResult));
    }
}