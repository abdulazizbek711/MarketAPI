using MarketApi.Dtos;
using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IProductMap
{
    public Product MapProduct(ProductDto productCreate);
    public Product MappProduct(int Product_ID, ProductDto updatedProduct);
}