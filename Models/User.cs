using System.ComponentModel.DataAnnotations;
namespace MarketApi.Models;
public class User
{
    [Key]
    public int User_ID { get; set; }

    public string UserName { get; set; } = "Abdulaziz";
    public int PhoneNumber { get; set; }
    public string Email { get; set; } = "panjiyevs@gmail.com";
    public List<Order> Orders { get; set; }
}