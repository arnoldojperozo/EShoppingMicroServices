using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository.Interfaces;

namespace Catalog.Application.Handlers.Queries;

public class GetProductsByBrandQueryHandler
{
    private readonly IProductRepository _productRepository;
    
    public GetProductsByBrandQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request,
        CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductByBrand(request.BrandName);

        var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);

        return productResponseList;
    }
}