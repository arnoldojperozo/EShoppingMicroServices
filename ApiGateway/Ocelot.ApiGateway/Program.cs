using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot().AddCacheManager(o => o.WithDictionaryHandle());

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    IEndpointConventionBuilder endpointConventionBuilder = endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Hello Ocelot");
//    });
//});


app.MapGet("/", () => Task.FromResult("Hello Ocelot"));

await app.UseOcelot();