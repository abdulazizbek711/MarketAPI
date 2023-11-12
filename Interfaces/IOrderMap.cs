using MarketApi.Dtos;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace MarketApi.Interfaces;
public interface IOrderMap
{
    public Order MapOrder(OrderDto orderCreate, [FromQuery]int User_ID);
    public Order MappOrder(int Order_number, OrderDto updatedOrder);
}