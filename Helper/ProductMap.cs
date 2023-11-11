using AutoMapper;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
namespace MarketApi.Helper;
public class ProductMap:IProductMap
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    public ProductMap(IMapper mapper, IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
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

}