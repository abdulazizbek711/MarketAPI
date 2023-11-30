using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Services;
public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly DataContext _context;

    public ProductService(IProductRepository productRepository, DataContext context)
    {
        _productRepository = productRepository;
        _context = context;
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
    public (bool, string) CreateProduct(Product product,  ProductDto productCreate)
    {
        if (productCreate == null)
        {
            return (false, "No Products Created");
        }
        var existingProduct = _productRepository.GetProducts()
            .FirstOrDefault(c => c.Product_type != null &&
                                 c.Product_type.Trim().ToUpper() == productCreate.Product_type.Trim().ToUpper());
        if (existingProduct != null)
        {
            return (false, "Product already exists");
        }
        _productRepository.CreateProduct(product);
        return (true, "Product created successfully");
    }
    public (bool, string) UpdateProduct(Product product, int Product_ID,  ProductDto updatedProduct)
    {
        if (updatedProduct == null || Product_ID != updatedProduct.Product_ID)
        {
            return (false, "No products updated");
        }
        var existingProduct = _productRepository.GetProduct(Product_ID);
        if (existingProduct == null)
            return (false, "No products found");
        _context.Entry(existingProduct).State = EntityState.Detached;
        existingProduct.Product_type = updatedProduct.Product_type ?? existingProduct.Product_type;
        existingProduct.Quantity = updatedProduct.Quantity ?? existingProduct.Quantity;
        existingProduct.Price_Amount= updatedProduct.Price_Amount ?? existingProduct.Price_Amount;
        existingProduct.Price_Currency = updatedProduct.Price_Currency ?? existingProduct.Price_Currency;
        _productRepository.UpdateProduct(product);
        return (true, "Product updated successfully");
    }
    public (bool, string) DeleteProduct(int Product_ID)
    {
        if (!_productRepository.ProductExists(Product_ID))
        {
            return (false, "Product not found");
        }
        var productToDelete = _productRepository.GetProduct(Product_ID);
        if (productToDelete == null)
        {
            return (false, "Product not exist");
        }
        _productRepository.DeleteProduct(productToDelete);
        return (true, "Product successfully deleted");
    }
}