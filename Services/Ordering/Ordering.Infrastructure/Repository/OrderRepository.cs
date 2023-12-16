﻿using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repository;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    
    public OrderRepository(OrderContext orderContext) : base(orderContext)
    {
        
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await _orderContext.Orders.Where(o => o.UserName == userName).ToListAsync();

        return orderList;
    }

}
