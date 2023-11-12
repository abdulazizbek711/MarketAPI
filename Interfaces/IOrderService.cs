using MarketApi.Dtos;
using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IOrderService
{
    IEnumerable<Order> GetOrders();
    public (bool, string) CreateOrder(Order order, OrderDto orderCreate);
    public (bool, string) UpdateOrder(Order order, int Order_number, OrderDto updatedOrder);
    public (bool, string) DeleteOrder(int Order_number);
}