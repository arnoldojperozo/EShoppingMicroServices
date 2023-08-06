using Catalog.Application.Commands;
using Catalog.Core.Repository.Interfaces;
using Catalog.Infrastructure.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands;

public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductByIdCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        return await _productRepository.DeleteProduct(request.Id);
    }
}