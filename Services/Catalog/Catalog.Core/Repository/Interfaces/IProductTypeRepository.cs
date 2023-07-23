using Catalog.Core.Entities;

namespace Catalog.Core.Repository.Interfaces;

public interface IProductTypeRepository
{
    Task<IEnumerable<ProductType>> GetAllProductTypes();
}