using System.Reflection;
using Catalog.Application.Commands;
using Catalog.Application.Handlers.Commands;
using Catalog.Core.Repository.Interfaces;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.Interfaces;
using Catalog.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

#region Dependency Injection Container
// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning();
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")!, "Catalog Mongo Db Health Check",
        HealthStatus.Degraded);
builder.Services.AddSwaggerGen();
#endregion Dependency Injection Container

#region Dependency Injection Components
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductBrandRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductRepository>();
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).GetTypeInfo().Assembly));
#endregion Dependency Injection Components


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication().UseRouting().UseStaticFiles().UseEndpoints(endpoints=> {
    endpoints.MapControllers();//,
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
