using Catalog.Core.Entities;

namespace Catalog.Core.Repository.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProduct(string id);
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProductByName(string name);
    Task<IEnumerable<Product>> GetProductByBrand(string brand);
    Task<IEnumerable<Product>> GetProductByType(string type);
    Task<Product> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}