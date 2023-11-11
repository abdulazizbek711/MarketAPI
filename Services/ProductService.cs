using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace MarketApi.Services;
public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public IEnumerable<Product> GetProducts()
    {
        var products = _productRepository.GetProducts();
        if (products == null || !products.Any())
        {
            throw new InvalidOperationException("No products found");
        }
        return products;
    }
    public (bool, string) CreateProduct(Product product, ProductDto productCreate)
    {
        if (productCreate == null)
        {
            return (false, "No Products Created");
        }
        var existingProduct = _productRepository.GetProducts()
            .FirstOrDefault(c => c.Product_type.Trim().ToUpper() == productCreate.Product_type.Trim().ToUpper());
        if (existingProduct != null)
        {
            return (false, "Product already exists");
        }
        _productRepository.CreateProduct(product);
        return (true, "Product created successfully");
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