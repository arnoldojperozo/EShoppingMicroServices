using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly ProductRepository _productRepository;

    public CreateProductCommandHandler(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = ProductMapper.Mapper.Map<Product>(request);

        if (productEntity is null)
        {
            throw new ApplicationException("There is an Issue with Mapping, creating a new product");
        }

        var newProduct = await _productRepository.CreateProduct(productEntity);

        var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);

        return productResponse;
    }
}