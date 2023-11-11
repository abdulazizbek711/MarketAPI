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

    public OrderMap(IMapper mapper, IOrderService orderService, IUserRepository userRepository)
    {
        _mapper = mapper;
        _orderService = orderService;
        _userRepository = userRepository;
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
}