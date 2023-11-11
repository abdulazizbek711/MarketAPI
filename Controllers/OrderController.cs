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
        public IActionResult UpdateOrder(int Order_ID, [FromBody] OrderDto updatedOrder)
        {
            if (updatedOrder == null || Order_ID != updatedOrder.Order_number || !ModelState.IsValid)
                return BadRequest(ModelState);
            var existingOrder = _orderRepository.GetOrder(Order_ID);
            if (existingOrder == null)
                return NotFound();
            _context.Entry(existingOrder).State = EntityState.Detached;
            existingOrder.User_ID = updatedOrder.User_ID ?? existingOrder.User_ID;
            existingOrder.Product_ID = updatedOrder.Product_ID ?? existingOrder.Product_ID;
            existingOrder.Quantity = updatedOrder.Quantity ?? existingOrder.Quantity;
            existingOrder.Price_Amount = updatedOrder.Price_Amount ?? existingOrder.Price_Amount;
            existingOrder.Price_Currency = updatedOrder.Price_Currency ?? existingOrder.Price_Currency;
            var orderMap = _mapper.Map<Order>(existingOrder);
            if (!_orderService.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{Order_number}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(int Order_number)
        {
            if (!_orderRepository.OrderExists(Order_number))
                return NotFound();
            var orderToDelete = _orderRepository.GetOrder(Order_number);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_orderService.DeleteOrder(orderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting order");
            }
            return NoContent();
        }
    }
}