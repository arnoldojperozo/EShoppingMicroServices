using Catalog.Core.Entities;
using Catalog.Core.Repository.Interfaces;
using Catalog.Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository, IProductBrandRepository, IProductTypeRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }
    public async Task<Product> GetProduct(string id)
    {
        return await _context.Products.Find(p => p.Id == id).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.Find(p => true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        
        return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByBrand(string brand)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, brand);
        
        return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByType(string type)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Types.Name, type);
        
        return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);

        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllProductBrands()
    {
        return await _context.ProductBrands.Find(p => true).ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllProductTypes()
    {
        return await _context.ProductTypes.Find(p => true).ToListAsync();
    }
}