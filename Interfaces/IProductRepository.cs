using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IProductRepository
{
    ICollection<Product> GetProducts();
    Product GetProduct(int Product_ID);
    Product GetProduct(string Product_type);
    bool ProductExists(int Product_ID);
    bool CreateProduct(Product product);
    bool UpdateProduct(Product product);
    bool DeleteProduct(Product product);
}