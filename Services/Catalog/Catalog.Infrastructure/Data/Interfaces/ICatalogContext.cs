using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Interfaces;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; set; }
    IMongoCollection<ProductBrand> ProductBrands { get; set; }
    IMongoCollection<ProductType> ProductTypes { get; set; }
}