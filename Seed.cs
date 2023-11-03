using MarketApi.Data;
using MarketApi.Models;
namespace MarketApi
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }
        public void SeedDataContext()
        {
            if (!_context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { UserName = "User1", Email = "user1@example.com" },
                    new User { UserName = "User2", Email = "user2@example.com" },
                };
                _context.Users.AddRange(users);
            }
            if (!_context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Product_type = "Product1",
                        Quantity = 5,
                        ProductQuantity_type =  Product.ProductQuantityType.Kilograms,
                        Price_Amount = 7.99,
                        Price_Currency = "$"
                    },
                };
                _context.Products.AddRange(products);
            }
            if (!_context.Orders.Any())
            {
                var orders = new List<Order>
                {
                    new Order
                    {
                        User_ID = _context.Users.First().User_ID,
                        Product_ID = _context.Products.First().Product_ID,
                        OrderQuantity_type = Order.OrderQuantityType.Kilograms,
                        Price_Amount = 5.99,
                        Price_Currency = "USD"
                    },
                };
                _context.Orders.AddRange(orders);
            }
            _context.SaveChanges();
        }
    }
}