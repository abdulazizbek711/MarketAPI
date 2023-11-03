using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IProductService
{
    IEnumerable<Product> GetProducts();
    bool CreateProduct(Product product);
    bool UpdateProduct(Product product);
    bool DeleteProduct(Product product);
}