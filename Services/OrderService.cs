using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Services;
public class OrderService:IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly DataContext _context;

    public OrderService(IOrderRepository orderRepository, DataContext context)
    {
        _orderRepository = orderRepository;
        _context = context;
    }
    public IEnumerable<Order> GetOrders()
    {
        var orders = _orderRepository.GetOrders();
        if (orders == null)
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
    public (bool, string) UpdateOrder(Order order, int Order_number,  OrderDto updatedOrder)
    {
        if (updatedOrder == null || Order_number != updatedOrder.Order_number)
        {
            return (false, "No orders updated");
        }
        var existingOrder = _orderRepository.GetOrder(Order_number);
        if (existingOrder == null)
            return (false, "No orders found");
        _context.Entry(existingOrder).State = EntityState.Detached;
        existingOrder.User_ID = updatedOrder.User_ID ?? existingOrder.User_ID;
        existingOrder.Product_ID = updatedOrder.Product_ID ?? existingOrder.Product_ID;
        existingOrder.Quantity = updatedOrder.Quantity ?? existingOrder.Quantity;
        existingOrder.Price_Amount = updatedOrder.Price_Amount ?? existingOrder.Price_Amount;
        existingOrder.Price_Currency = updatedOrder.Price_Currency ?? existingOrder.Price_Currency;
        _orderRepository.UpdateOrder(order);
        return (true, "Order updated successfully");
    }
    public (bool, string) DeleteOrder(int Order_number)
    {
        if (!_orderRepository.OrderExists(Order_number))
        {
            return (false, "Order not found");
        }
        var orderToDelete = _orderRepository.GetOrder(Order_number);
        if (orderToDelete == null)
        {
            return (false, "Order not exist");
        }
        _orderRepository.DeleteOrder(orderToDelete);
        return (true, "Order succesfully deleted");
    }
}