using Basket.Application.Queries;
using Basket.Core.Repositories.Interfaces;
using MediatR;

namespace Basket.Application.Handlers.Queries;

public class DeleteBasketByUserNameQueryHandler : IRequestHandler<DeleteBasketByUserNameQuery>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketByUserNameQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task Handle(DeleteBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasket(request.UserName);
    }
}