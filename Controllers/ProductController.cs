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
        private readonly DataContext _context;
        private readonly IProductMap _productMap;

        public ProductController(IProductRepository productRepository,  IProductService productService, IMapper mapper, DataContext context, IProductMap productMap)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
            _context = context;
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
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok(productCreate);
        }
        [HttpPut("{Product_ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int Product_ID, [FromBody] ProductDto updatedProduct)
        {
            if (updatedProduct == null || Product_ID != updatedProduct.Product_ID || !ModelState.IsValid)
                return BadRequest(ModelState);
            var existingProduct = _productRepository.GetProduct(Product_ID);
            if (existingProduct == null)
                return NotFound();
            _context.Entry(existingProduct).State = EntityState.Detached;
            existingProduct.Product_type = updatedProduct.Product_type ?? existingProduct.Product_type;
            existingProduct.Quantity = updatedProduct.Quantity ?? existingProduct.Quantity;
            existingProduct.Price_Amount= updatedProduct.Price_Amount ?? existingProduct.Price_Amount;
            existingProduct.Price_Currency = updatedProduct.Price_Currency ?? existingProduct.Price_Currency;
            var productMap = _mapper.Map<Product>(existingProduct);
            if (!_productService.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{Product_ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int Product_ID)
        {
            if (!_productRepository.ProductExists(Product_ID))
                return NotFound();
            var productToDelete = _productRepository.GetProduct(Product_ID);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_productService.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }
            return NoContent();
        }
    }
}