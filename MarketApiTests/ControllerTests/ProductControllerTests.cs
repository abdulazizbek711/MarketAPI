using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MarketApi.Controllers;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace MarketApi.MarketApiTests.ControllerTests;
public class ProductControllerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;
    private readonly IProductMap _productMap;
    private readonly IMapper _mapper;
    public ProductControllerTests()
    {
        _productRepository = A.Fake<IProductRepository>();
        _productService = A.Fake<IProductService>();
        _productMap = A.Fake<IProductMap>();
        _mapper = A.Fake<IMapper>();
    }
    [Fact]
    public void ProductController_GetProducts_ReturnsOkObjectResult()
    {
        //Arrange
        var products = A.CollectionOfDummy<ProductDto>(3); 
        var productList = A.CollectionOfDummy<ProductDto>(3).ToList(); 
        A.CallTo(() => _mapper.Map<List<ProductDto>>(products)).Returns(productList);
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.GetProducts();
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
    }
    [Fact]
    public void ProductController_CreateProduct_ReturnsOkObjectResult()
    {
        //Arrange
        var product = A.Fake<ProductDto>();
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.CreateProduct(product);
        //Assert
        result.Should().NotBeNull();
        if (result is NoContentResult noContentResult)
        {
            noContentResult.StatusCode.Should().Be(204); 
        }
        else if (result is BadRequestObjectResult badRequest)
        {
            badRequest.StatusCode.Should().Be(400);
        }
    }
    [Fact]
    public void ProductController_UpdateProduct_ReturnsOkObjectResult()
    {
        //Arrange
        var Product_ID = 1;
        var product = A.Fake<ProductDto>();
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.UpdateProduct(Product_ID, product);
        //Assert
        result.Should().NotBeNull();
        if (result is NoContentResult noContentResult)
        {
            noContentResult.StatusCode.Should().Be(204); 
        }
        else if (result is BadRequestObjectResult badRequest)
        {
            badRequest.StatusCode.Should().Be(400);
        }
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void ProductController_DeleteProduct_ReturnsOkObjectResult(int Product_ID)
    {
        //Arrange
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.DeleteProduct(Product_ID);
        //Assert
        result.Should().NotBeNull();
        if (result is NoContentResult noContentResult)
        {
            noContentResult.StatusCode.Should().Be(204); 
        }
        else if (result is BadRequestObjectResult badRequest)
        {
            badRequest.StatusCode.Should().Be(400);
        }
    }
}