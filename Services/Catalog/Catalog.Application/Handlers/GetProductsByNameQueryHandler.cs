﻿using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductsResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IList<ProductsResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductByName(request.Name);

        var productResponseList = ProductMapper.Mapper.Map<IList<ProductsResponse>>(productList);

        return productResponseList;
    }
}