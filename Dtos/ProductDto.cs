using System.ComponentModel.DataAnnotations;
using MarketApi.Models;
namespace MarketApi.Dtos;
public class ProductDto
{
    [Key]
    public int Product_ID { get; set; }
    public string? Product_type { get; set; }
    public decimal? Quantity { get; set; }
    public double? Price_Amount { get; set; }
    public string? Price_Currency { get; set; }
    
    
}