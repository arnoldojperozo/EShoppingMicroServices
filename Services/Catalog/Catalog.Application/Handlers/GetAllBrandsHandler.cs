using AutoMapper;
using Catalog.Application.Models;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllBrandsHandler:IRequestHandler<GetAllBrandsQuery, IList<BrandsResponse>>
{
    private readonly IProductBrandRepository _brandRepository;
    private readonly IMapper _productMapper;

    public GetAllBrandsHandler(IProductBrandRepository brandRepository, IMapper productMapper)
    {
        _brandRepository = brandRepository;
        _productMapper = productMapper;
    }

    public async Task<IList<BrandsResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllProductBrands();

        var brandResponseList = _productMapper.Map<IList<BrandsResponse>>(brandList);

        return brandResponseList;
    }
}