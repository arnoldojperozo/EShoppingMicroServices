using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllBrandsHandler:IRequestHandler<GetAllBrandsQuery, IList<BrandsResponse>>
{
    private readonly IProductBrandRepository _brandRepository;

    public GetAllBrandsHandler(IProductBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IList<BrandsResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllProductBrands();

        var brandResponseList =
            ProductMapper.Mapper.Map<IList<ProductBrand>, IList<BrandsResponse>>(brandList.ToList());
        return brandResponseList;
    }
}