using FakeItEasy;
using FluentAssertions;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Models;
using MarketApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace MarketApi.MarketApiTests.RepositoryTests;
public class ProductRepositoryTests
{
    private async Task<DataContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new DataContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Products.CountAsync() <= 0)
        {
            for (int i = 1; i <= 10; i++)
            {
                databaseContext.Products.Add(
                    new Product()
                    {
                        Product_type = "Apple",
                        Product_ID = i,
                        Quantity = 5,
                        ProductQuantity_type = Product.ProductQuantityType.Kilograms,
                        Price_Amount = 5,
                        Price_Currency = "$"
                    });
                await databaseContext.SaveChangesAsync();
            }
        }
        return databaseContext;
    }
    [Fact]
    public async  Task ProductRepository_GetProducts_ReturnsProducts()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var productRepository = new ProductRepository(dbContext);
        //Act
        var result = productRepository.GetProducts();
        //Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public async Task ProductRepository_GetProduct_ReturnProductByProduct_type()
    {
        //Arrange
        var Product_type = "Apple";
        var dbContext = await GetDatabaseContext();
        var productRepository = new ProductRepository(dbContext);
        //Act
        var result = productRepository.GetProduct(Product_type);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Product>();
    }
    [Fact]
    public async Task ProductRepository_GetProduct_ReturnProductByProduct_ID()
    {
        //Arrange
        var Product_ID = 2;
        var dbContext = await GetDatabaseContext();
        var productRepository = new ProductRepository(dbContext);
        //Act
        var result = productRepository.GetProduct(Product_ID);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Product>();
    }
    [Fact]
    public async Task ProductRepository_ProductExists_ReturnBool()
    {
        //Arrange
        var Product_ID = 2;
        var dbContext = await GetDatabaseContext();
        var productRepository = new ProductRepository(dbContext);
        //Act
        var result = productRepository.ProductExists(Product_ID);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task ProductRepository_CreateProduct_ReturnBool()
    {
        //Arrange
        var product = A.Fake<Product>();
        product.Price_Currency = "$"; // Set required properties
        product.Product_type = "Banana"; // Set required properties
        var dbContext = await GetDatabaseContext();
        var productRepository = new ProductRepository(dbContext);
        //Act
        var result = productRepository.CreateProduct(product);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task ProductRepository_UpdateProduct_ReturnBool()
    {
        //Arrange
        var product = A.Fake<Product>();
        product.Price_Currency = "$"; // Set required properties
        product.Product_type = "Mango"; // Set required properties
        var dbContext = await GetDatabaseContext();
        var productRepository = new ProductRepository(dbContext);
        //Act
        var result = productRepository.UpdateProduct(product);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task ProductRepository_DeleteProduct_ReturnBool()
    {
        //Arrange
        var product = A.Fake<Product>();
        var dbContext = await GetDatabaseContext();
        var productRepository = new ProductRepository(dbContext);
        //Act
        var result = productRepository.DeleteProduct(product);
        //Assert
        result.Should().BeFalse();
    }
}