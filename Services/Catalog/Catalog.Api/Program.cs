using System.Reflection;
using Catalog.Application.Handlers.Commands;
using Catalog.Core.Repository.Interfaces;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.Interfaces;
using Catalog.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning();
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"), "Catalog Mongo Db Health Check",
        HealthStatus.Degraded);
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(CreateProductCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductBrandRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductRepository>();


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
    //endpoints.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions{
    //    Predicate= _ = true,
    //    ResponseWriter = UIR
    //}
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
