using MarketApi.Dtos;
using MarketApi.Models;
namespace MarketApi.Interfaces;
public interface IProductService
{
    IEnumerable<Product> GetProducts();
    public (bool, string) CreateProduct(Product product, ProductDto productCreate);
    public (bool, string) UpdateProduct(Product product, int Product_ID, ProductDto productCreate);
    public (bool, string) DeleteProduct(int Product_ID);
}