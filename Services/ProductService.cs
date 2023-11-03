using AutoMapper;
using MarketApi.Interfaces;
using MarketApi.Models;
namespace MarketApi.Services;
public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public IEnumerable<Product> GetProducts()
    {
        return _productRepository.GetProducts();
    }
    public bool CreateProduct(Product product)
    {
        return _productRepository.CreateProduct(product);
    }
    public bool UpdateProduct(Product product)
    {
        return _productRepository.UpdateProduct(product);
    }
    public bool DeleteProduct(Product product)
    {
        return _productRepository.DeleteProduct(product);
    }
}