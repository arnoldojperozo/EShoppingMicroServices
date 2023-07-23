using Catalog.Core.Entities;

namespace Catalog.Core.Repository.Interfaces;

public interface IProductBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllProductBrands();
}