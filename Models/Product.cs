using System.ComponentModel.DataAnnotations;
namespace MarketApi.Models;
public class Product
{
    [Key]
    public int Product_ID { get; set; }

    public string Product_type { get; set; } = "Apple";
    public decimal Quantity { get; set; }
    public ProductQuantityType ProductQuantity_type { get; set; }
    public double Price_Amount { get; set; }
    public string Price_Currency { get; set; } = "USD";
    public List<Order> Orders { get; set; }
    public enum ProductQuantityType
    {
        Kilograms,
        Pieces
    }
}