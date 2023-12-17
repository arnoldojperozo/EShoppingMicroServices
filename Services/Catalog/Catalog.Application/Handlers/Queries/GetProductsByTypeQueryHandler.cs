using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetProductsByTypeQueryHandler : IRequestHandler<GetProductsByTypeQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    
    public GetProductsByTypeQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByTypeQuery request,
        CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductByType(request.TypeName);

        var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);

        return productResponseList;
    }
}