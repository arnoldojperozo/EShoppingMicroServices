using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetOrders());

            await orderContext.SaveChangesAsync();
            logger.LogInformation($"Ordering Database Seeded: {typeof(OrderContext).Name}");
        }
    }

    private static IEnumerable<Order> GetOrders()
    {
        return new List<Order>
        {
            new Order
            {
                UserName = "Me",
                FirstName = "Arnoldo",
                LastName = "Perozo",
                EmailAddress = "aperozo@eshop.net",
                AddressLine = "Davenport",
                Country = "USA",
                TotalPrice = 750,
                State = "FL",
                ZipCode = "33897",

                CardName = "Visa",
                CardNumber = "1234567890123456",
                CreatedBy = "Me",
                Expiration = "12/25",
                Cvv = "123",
                PaymentMethod = 1,
                LastModifiedBy = "Me",
                LastModifiedDate = new DateTime()
            }
        };
    }
}
