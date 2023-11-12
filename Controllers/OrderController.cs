using AutoMapper;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;
        private readonly IOrderMap _orderMap;
        private readonly IProductRepository _productRepository;
        public OrderController(IMapper mapper,IOrderService orderService, IOrderRepository orderRepository,IUserRepository userRepository, DataContext context, IOrderMap orderMap, IProductRepository productRepository)
        {
            _mapper = mapper;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _context = context;
            _orderMap = orderMap;
            _productRepository = productRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }
        public static class OrderNumberGenerator
        {
            private static int counter = 100;
            public static int GenerateOrderNumber()
            {
                return counter++;
            }
        }
        [HttpPost("{Order_number}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder(int User_ID,  OrderDto orderCreate)
        {
            User currentUser = _userRepository.GetUser(User_ID); 
            var orderMap = _orderMap.MapOrder(orderCreate, currentUser.User_ID);
            (bool success, string message) result = _orderService.CreateOrder(orderMap, orderCreate);
            if (!result.success)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok(orderCreate);
        }
        [HttpPut("{Order_number}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrder(int Order_number, [FromBody] OrderDto updatedOrder)
        {
            var orderMap = _orderMap.MappOrder(Order_number, updatedOrder);
            (bool success, string message) result = _orderService.UpdateOrder(orderMap, Order_number, updatedOrder);
            if (!result.success)
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return Ok(updatedOrder);
        }
        [HttpDelete("{Order_number}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(int Order_number)
        {
            var orderToDelete = _orderRepository.GetOrder(Order_number);
            (bool success, string message) result = _orderService.DeleteOrder(Order_number);
            if (!result.success)
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}