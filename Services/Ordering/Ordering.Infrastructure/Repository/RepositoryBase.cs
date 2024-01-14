using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Repository;
using Ordering.Infrastructure.Data;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repository;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly OrderContext _orderContext;

    public RepositoryBase(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }
    public async Task<T> AddAsync(T entity)
    {
        await _orderContext.AddAsync(entity);

        await _orderContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _orderContext.Set<T>().Remove(entity);
        await _orderContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _orderContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _orderContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _orderContext.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task UpdateAsync(T entity)
    {
        _orderContext.Entry(entity).State = EntityState.Modified;
        await _orderContext.SaveChangesAsync();
    }
}
