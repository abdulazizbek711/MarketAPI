using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MarketApi.Controllers;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
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
        //Assign
        var products = A.CollectionOfDummy<ProductDto>(3); // Use A.CollectionOfDummy to create a collection of fake ProductDto
        var productList = A.CollectionOfDummy<ProductDto>(3).ToList(); // Convert t
        A.CallTo(() => _mapper.Map<List<ProductDto>>(products)).Returns(productList);
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.GetProducts();
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void ProductController_CreateProduct_ReturnsOkObjectResult()
    {
        //Assign
        var product = A.Fake<ProductDto>();
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.CreateProduct(product);
        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void ProductController_UpdateProduct_ReturnsOkObjectResult()
    {
        //Assign
        var Product_ID = 1;
        var product = A.Fake<ProductDto>();
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.UpdateProduct(Product_ID, product);
        //Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void ProductController_DeleteProduct_ReturnsOkObjectResult(int Product_ID)
    {
        //Assign
        var controller = new ProductController(_productRepository, _productService, _mapper, _productMap);
        //Act
        var result = controller.DeleteProduct(Product_ID);
        //Assert
        result.Should().BeOfType(typeof(ObjectResult));
    }
}