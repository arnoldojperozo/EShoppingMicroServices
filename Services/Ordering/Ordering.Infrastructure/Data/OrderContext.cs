using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
        
    }

    public DbSet<Order> Orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var item in ChangeTracker.Entries<EntityBase>())
        {
            switch (item.State)
            {
                case EntityState.Added:
                    item.Entity.CreatedBy = "Me";                   //Will be replaced by IDS
                    item.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    item.Entity.LastModifiedDate = DateTime.Now;
                    item.Entity.LastModifiedBy = "MeToo";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
