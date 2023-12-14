using Ordering.Core.Entities;

namespace Ordering.Core.Repository;

public interface IOrderRepository : IAsyncRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
}
