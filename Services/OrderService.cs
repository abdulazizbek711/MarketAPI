using AutoMapper;
using MarketApi.Interfaces;
using MarketApi.Models;
namespace MarketApi.Services;
public class OrderService:IOrderService
{
    private readonly IOrderRepository _orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public IEnumerable<Order> GetOrders()
    {
        return _orderRepository.GetOrders();
    }
    public bool CreateOrder(Order order)
    {
        return _orderRepository.CreateOrder(order);
    }
    public bool UpdateOrder(Order order)
    {
        return _orderRepository.UpdateOrder(order);
    }
    public bool DeleteOrder(Order order)
    {
        return _orderRepository.DeleteOrder(order);
    }
}