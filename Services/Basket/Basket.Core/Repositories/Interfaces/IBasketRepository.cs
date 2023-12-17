
using Basket.Core.Entities;

namespace Basket.Core.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);

    Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);

    Task DeleteBasket(string userName);
}

