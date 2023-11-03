using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IOrderService
{
    IEnumerable<Order> GetOrders();
    bool CreateOrder(Order order);
    bool UpdateOrder(Order order);
    bool DeleteOrder(Order order);
}