using System.ComponentModel.DataAnnotations;

namespace MarketApi.Dtos;

public class OrderDto
{
    [Key]
    public int Order_number { get; set; }
    
    public int? Product_ID { get; set; }
    public decimal? Quantity { get; set; }
    public int? User_ID { get; set; }
    public double? Price_Amount { get; set; }
    public string? Price_Currency { get; set; }
}