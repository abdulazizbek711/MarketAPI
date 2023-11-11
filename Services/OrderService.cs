using MarketApi.Dtos;
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
        var orders = _orderRepository.GetOrders();
        if (orders == null || !orders.Any())
        {
            throw new InvalidOperationException("No orders found");
        }
        return orders;
    }
    public (bool, string) CreateOrder(Order order, OrderDto orderCreate)
    {
        if (orderCreate == null)
        {
            return (false, "No Orders Created");
        }
        var existingOrder = _orderRepository.GetOrders()
            .Where(c => c.Order_number.ToString().ToUpper() == orderCreate.Order_number.ToString().ToUpper())
            .FirstOrDefault();
        if (existingOrder != null)
        {
            return (false, "Order already exists");
        }
        _orderRepository.CreateOrder(order);
        return (true, "Order created successfully");
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