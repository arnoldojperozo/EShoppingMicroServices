using System.Reflection;
using Basket.Application.GrpcService;
using Basket.Application.Handlers.Commands;
using Basket.Core.Repositories.Interfaces;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning();
builder.Services.AddSwaggerGen();

//Redis Settings
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateShoppingCartCommandHandler).GetTypeInfo().Assembly));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")));
builder.Services.AddHealthChecks().AddRedis(builder.Configuration["CacheSettings:ConnectionString"], "Redis Health",
    HealthStatus.Degraded);

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();
    config.UsingRabbitMq((ct, cf) =>
    {
        //cf.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cf.Host(new Uri(builder.Configuration["EventBusSettings:Host"]!), c =>
        {
            c.Username(builder.Configuration["EventBusSettings:UserName"]);
            c.Username(builder.Configuration["EventBusSettings:Password"]);
        });

        cf.ConfigureEndpoints(ct);
    });
});

//builder.Services.AddMassTransit(mt => mt.AddMassTransit(x => {
//    x.UsingRabbitMq((cntxt, cfg) => {
//        cfg.Host(builder.Configuration["EventBusSettings:Host"], "/", c => {
//            c.Username(builder.Configuration["EventBusSettings:UserName"]);
//            c.Password(builder.Configuration["EventBusSettings:Password"]);
//        });
//    });
//}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.MapControllers();
app.Run();


