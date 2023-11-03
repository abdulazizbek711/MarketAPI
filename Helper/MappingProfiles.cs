using AutoMapper;
using MarketApi.Dtos;
using MarketApi.Models;
namespace MarketApi.Helper;
public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();
    }
}