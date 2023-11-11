using MarketApi.Dtos;
using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IProductService
{
    IEnumerable<Product> GetProducts();
    public (bool, string) CreateProduct(Product product, ProductDto productCreate);
    bool UpdateProduct(Product product);
    bool DeleteProduct(Product product);
}