using System.ComponentModel.DataAnnotations;
namespace MarketApi.Models;
public class User
{
    [Key]
    public int User_ID { get; set; }
    public string UserName { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<Order> Orders { get; set; }
}