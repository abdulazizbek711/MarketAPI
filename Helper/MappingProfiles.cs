using System.Web.Http.ModelBinding;
using AutoMapper;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;

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