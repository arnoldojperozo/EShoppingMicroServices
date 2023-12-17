using EventBus.Messages.Common;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.Api.EventBusConsumer;
using Ordering.Api.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks().Services.AddDbContext<OrderContext>();

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();
    //Mark as Consumer
    config.AddConsumer<BasketOrderingConsumer>();

    config.UsingRabbitMq((ct,cf) =>
    {
        cf.Host(new Uri(builder.Configuration["EventBusSettings:Host"]!), c =>
        {
            c.Username(builder.Configuration["EventBusSettings:UserName"]);
            c.Username(builder.Configuration["EventBusSettings:Password"]);
        });
        cf.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c=> {
            c.ConfigureConsumer<BasketOrderingConsumer>();
        });
        cf.ConfigureEndpoints(ct);
    });
});

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();

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
