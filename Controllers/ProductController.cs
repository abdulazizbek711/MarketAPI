using AutoMapper;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Helper;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IProductMap _productMap;

        public ProductController(IProductRepository productRepository,  IProductService productService, IMapper mapper, IProductMap productMap)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
            _productMap = productMap;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }
        [HttpPost("{Product_ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct(ProductDto productCreate)
        {
            var productMap = _productMap.MapProduct(productCreate);
            (bool success, string message) result = _productService.CreateProduct(productMap, productCreate);
            if (!result.success)
            {
                ModelState.AddModelError("", "Something went wrong while savin"); // Use the message from the service
                return BadRequest(ModelState); // Return a 400 Bad Request for client errors
            }

            return Ok(productCreate);
        }
        [HttpPut("{Product_ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int Product_ID, [FromBody] ProductDto updatedProduct)
        {
            var productMap = _productMap.MappProduct(Product_ID, updatedProduct);
            (bool success, string message) result = _productService.UpdateProduct(productMap, Product_ID, updatedProduct);
            if (!result.success)
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return BadRequest(ModelState);
            }
            return Ok(updatedProduct);
        }
        [HttpDelete("{Product_ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int Product_ID)
        {
            var productToDelete = _productRepository.GetProduct(Product_ID);
            (bool success, string message) result = _productService.DeleteProduct(Product_ID);
            if (!result.success)
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}