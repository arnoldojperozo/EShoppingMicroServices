using Basket.Application.Mappers;
using Basket.Core.Repositories.Interfaces;
using MediatR;

namespace Basket.Application.Handlers.Queries;
public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;

    public GetBasketByUserNameQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository=basketRepository;
    }

    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basketRepository.GetBasket(request.UserName);

        var shoppingCartResponse=BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);

        return shoppingCartResponse;
    }
}
