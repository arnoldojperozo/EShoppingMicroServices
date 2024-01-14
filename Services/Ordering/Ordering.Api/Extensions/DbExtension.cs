using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Ordering.Api.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
        using (var scope=host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger=services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation($"Started Db Migration: {typeof(TContext).Name}");
                //retry strategy
                var retry = Policy.Handle<SqlException>()
                    .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (exception, span, cont) =>
                        {
                            logger.LogError("Retrying because of {exception} {retry}", exception, span);
                        });
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
                retry.Execute(() => CallSeeder(seeder, context, services));
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                logger.LogInformation($"Migration Completed: {typeof(TContext).Name}");
            }
            catch (SqlException e)
            {
                logger.LogError(e, $"An error occurred while migrating db: {typeof(TContext).Name}");
            }
        }

        return host;
    }

    private static int CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);

        return 1;
    }
}
