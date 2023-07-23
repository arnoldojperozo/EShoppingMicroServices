using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IList<ProductBrandResponse>>
{
    private readonly IProductBrandRepository _brandRepository;

    public GetAllBrandsQueryHandler(IProductBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IList<ProductBrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllProductBrands();

        var brandResponseList =
            ProductMapper.Mapper.Map<IList<ProductBrand>, IList<ProductBrandResponse>>(brandList.ToList());
        return brandResponseList;
    }
}