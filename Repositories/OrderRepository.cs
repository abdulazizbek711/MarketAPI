using MarketApi.Data;
using MarketApi.Interfaces;
using MarketApi.Models;
namespace MarketApi.Repositories;
public class OrderRepository: IOrderRepository
{
    private readonly DataContext _context;
    private IOrderRepository _orderRepositoryImplementation;
    public OrderRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Order> GetOrders()
    {
        return _context.Orders.OrderBy(o => o.Order_number).ToList();
    }
    public Order GetOrder(int Order_number)
    {
        return _context.Orders.Where(p => p.Order_number==Order_number).FirstOrDefault();
    }
    public bool OrderExists(int Order_number)
    {
        return _context.Orders.Any(u => u.Order_number == Order_number);
    }
    public bool CreateOrder(Order order)
    {
        _context.Add(order);
        return Save();
    }
    public bool UpdateOrder(Order order)
    {
        _context.Update(order);
        return Save();
    }
    public bool DeleteOrder(Order order)
    {
        _context.Remove(order);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}