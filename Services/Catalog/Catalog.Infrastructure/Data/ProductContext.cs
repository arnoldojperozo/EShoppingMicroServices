using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class ProductContext
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool products = productCollection.Find(b => true).Any();
        string path = Path.Combine("Data", "SeedData", "products.json");

        if (!products)
        {
            var productsData = File.ReadAllText(path);
            var productList = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (productList != null)
            {
                foreach (var p in productList)
                {
                    productCollection.InsertOneAsync(p);
                }
            }
        }

    }
}