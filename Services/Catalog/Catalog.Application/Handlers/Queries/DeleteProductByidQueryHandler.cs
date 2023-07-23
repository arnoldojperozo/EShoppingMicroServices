using Catalog.Application.Queries;
using Catalog.Infrastructure.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class DeleteProductByidQueryHandler : IRequestHandler<DeleteProductByIdQuery, bool>
{
    private readonly ProductRepository _productRepository;

    public DeleteProductByidQueryHandler(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.DeleteProduct(request.Id);
    }
}