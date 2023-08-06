using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repository.Interfaces;
using Catalog.Infrastructure.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await _productRepository.UpdateProduct(new Product
        {
            Id = request.Id,
            Description = request.Description,
            Imagefile = request.ImageFile,
            Name = request.Name,
            Price = request.Price,
            Summary = request.Summary,
            Brands = request.Brands,
            Types = request.Types
        });

        return productEntity;
    }
}