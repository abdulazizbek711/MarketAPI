using AutoMapper;
using MarketApi.Controllers;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace MarketApi.Helper;
public class OrderMap: IOrderMap
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderMap(IMapper mapper, IOrderService orderService, IUserRepository userRepository, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderService = orderService;
        _userRepository = userRepository;
        _orderRepository = orderRepository;
    }
    public Order MapOrder(OrderDto orderCreate, int User_ID)
    {
        var orderMap = _mapper.Map<Order>(orderCreate);
        orderMap.User = _userRepository.GetUser(User_ID);
        orderMap.Order_number = OrderController.OrderNumberGenerator.GenerateOrderNumber();
        if (orderMap == null)
        {
            throw new InvalidOperationException("Something went wrong while saving");
        }
        return orderMap;
    }
    public Order MappOrder(int Order_number, OrderDto updatedOrder)
    {
        var existingOrder = _orderRepository.GetOrder(Order_number);
        var orderMap = _mapper.Map<Order>(existingOrder);
        var updateResult = _orderService.UpdateOrder(orderMap, Order_number, updatedOrder);
        if (!updateResult.Item1)
        {
            throw new InvalidOperationException($"Failed to update order: {updateResult.Item2}");
        }
        return orderMap;
    }
}