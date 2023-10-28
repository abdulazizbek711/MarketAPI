using System.ComponentModel.DataAnnotations;

namespace MarketApi.Dtos;

public class UserDto
{
    [Key]
    public int User_ID { get; set; }
    public string? UserName { get; set; }
    public int? PhoneNumber { get; set; }
    public string? Email { get; set; }
}