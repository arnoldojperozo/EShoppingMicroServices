using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behavior;
using Ordering.Application.Commands;
using System.Reflection;

namespace Ordering.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CheckoutOrderCommand).GetTypeInfo().Assembly));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>, typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>, typeof(UnhandledExceptionBehaviour<,>));

        return services;
    }

}
