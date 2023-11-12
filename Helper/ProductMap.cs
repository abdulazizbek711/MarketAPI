using AutoMapper;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
namespace MarketApi.Helper;
public class ProductMap:IProductMap
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;

    public ProductMap(IMapper mapper, IProductService productService, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productService = productService;
        _productRepository = productRepository;
    }
    public Product MapProduct(ProductDto productCreate)
    {
        var productMap = _mapper.Map<Product>(productCreate);
        if (productMap == null)
        {
            throw new InvalidOperationException("Something went wrong while saving");
        }
        return productMap;
    }
    public Product MappProduct(int Product_ID, ProductDto updatedProduct)
    {
        var existingProduct = _productRepository.GetProduct(Product_ID);
        var productMap = _mapper.Map<Product>(existingProduct);
        var updateResult = _productService.UpdateProduct(productMap, Product_ID, updatedProduct);
        if (!updateResult.Item1)
        {
            throw new InvalidOperationException($"Failed to update product: {updateResult.Item2}");
        }
        return productMap;
    }

}