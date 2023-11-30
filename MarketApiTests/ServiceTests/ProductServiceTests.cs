using FakeItEasy;
using FluentAssertions;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MarketApi.MarketApiTests.ServiceTests;

public class ProductServiceTests
{
    private readonly IProductRepository _productRepository;
    public ProductServiceTests()
    {
        _productRepository = A.Fake<IProductRepository>();
    }
    [Fact]
    public void ProductService_GetProducts_ReturnProducts()
    {
        // Arrange
        var fakeProductRepository = A.Fake<IProductRepository>();
        A.CallTo(() => fakeProductRepository.GetProducts()).Returns(new List<Product> { new Product(), new Product() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>(/* you can pass options here if needed */);
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var productService = new ProductService(fakeProductRepository, fakeDbContext);
        // Act
        var result = productService.GetProducts();
        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void ProductService_CreateProduct_ReturnOkObjectResult()
    {
        // Arrange
        var productCreate = new ProductDto { Product_type = "kiwi" };
        var product = new Product { Product_type = "kiwi" };
        var fakeProductRepository = A.Fake<IProductRepository>();
        A.CallTo(() => fakeProductRepository.GetProducts()).Returns(new List<Product> { new Product(), new Product() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>(/* you can pass options here if needed */);
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var productService = new ProductService(fakeProductRepository, fakeDbContext);
        //Act
        var result = productService.CreateProduct(product, productCreate);
        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void ProductService_UpdateProduct_ReturnOkObjectResult()
    {
        //Arrange
        var Product_ID = 40;
        var updatedProduct = new ProductDto { Product_type = "kiwi" };
        var product = new Product { Product_type = "kiwi" };
        var fakeProductRepository = A.Fake<IProductRepository>();
        A.CallTo(() => fakeProductRepository.GetProducts()).Returns(new List<Product> { new Product(), new Product() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>(/* you can pass options here if needed */);
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var productService = new ProductService(fakeProductRepository, fakeDbContext);
        // Act
        var result = productService.UpdateProduct(product, Product_ID, updatedProduct);
        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void ProductService_DeleteProduct_ReturnOkObjectResult()
    {
        // Arrange
        var Product_ID = 40;
        var fakeProductRepository = A.Fake<IProductRepository>();
        A.CallTo(() => fakeProductRepository.GetProducts()).Returns(new List<Product> { new Product(), new Product() });
        var fakeDbContextOptions = new DbContextOptions<DataContext>(/* you can pass options here if needed */);
        var fakeDbContext = A.Fake<DataContext>(x => x.WithArgumentsForConstructor(new[] { fakeDbContextOptions }));
        var productService = new ProductService(fakeProductRepository, fakeDbContext);
        // Act
        var result = productService.DeleteProduct(Product_ID);
        // Assert
        result.Should().NotBeNull();
    }
}