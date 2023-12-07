using System.ComponentModel.DataAnnotations;
namespace MarketApi.Models;
public class Order
{
    [Key]
    public int Order_number { get; set; }
    public int Product_ID { get; set; }
    public decimal Quantity { get; set; }
    public int User_ID { get; set; }
    public double Price_Amount { get; set; }
    public string Price_Currency { get; set; } = "USD";
    public User User { get; set; }
    public List<Product> Products { get; set; }
    public OrderQuantityType OrderQuantity_type { get; set; }
    public enum OrderQuantityType
    {
        Kilograms,
        Pieces
    }
}