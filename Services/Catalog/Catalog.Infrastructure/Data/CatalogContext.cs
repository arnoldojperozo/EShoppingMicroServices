using Catalog.Core.Entities;
using Catalog.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; set; }
    public IMongoCollection<ProductBrand> ProductBrands { get; set; }
    public IMongoCollection<ProductType> ProductTypes { get; set; }

    public CatalogContext(IConfiguration config)
    {
        var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));
        ProductBrands = database.GetCollection<ProductBrand>(config.GetValue<string>("DatabaseSettings:BrandsCollection"));
        ProductTypes = database.GetCollection<ProductType>(config.GetValue<string>("DatabaseSettings:TypesCollection"));
        Products = database.GetCollection<Product>(config.GetValue<string>("DatabaseSettings:CollectionName"));
        
        ProductBrandsContext.SeedData(ProductBrands);
        ProductTypesContext.SeedData(ProductTypes);
        ProductContext.SeedData(Products);
    }
}