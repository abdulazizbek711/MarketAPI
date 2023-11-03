using System.Xml.Linq;
using MarketApi.Data;
using MarketApi.Interfaces;
using MarketApi.Models;
namespace MarketApi.Repositories;
public class ProductRepository: IProductRepository
{
    private readonly DataContext _context;
    public ProductRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Product> GetProducts()
    {
        return _context.Products.OrderBy(p => p.Product_ID).ToList();
    }
    public Product GetProduct(int Product_ID)
    {
        return _context.Products.Where(p => p.Product_ID==Product_ID).FirstOrDefault();
    }
    public Product GetProduct(string Product_type)
    {
        return _context.Products.Where(p => p.Product_type==Product_type).FirstOrDefault();
    }
    public bool ProductExists(int Product_ID)
    {
        return _context.Products.Any(u => u.Product_ID == Product_ID);
    }
    public bool CreateProduct(Product product)
    {
        _context.Add(product);
        return Save();
    }
    public bool UpdateProduct(Product product)
    {
        _context.Update(product);
        return Save();
    }
    public bool DeleteProduct(Product product)
    {
        _context.Remove(product);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}