using MarketApi.Dtos;
using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IOrderService
{
    IEnumerable<Order> GetOrders();
    public (bool, string) CreateOrder(Order order, OrderDto orderCreate);
    bool UpdateOrder(Order order);
    bool DeleteOrder(Order order);
}