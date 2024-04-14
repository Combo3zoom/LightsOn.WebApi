using LightsOn.Application;
using LightsOn.WebApi;
using LightsOn.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyVaultIfConfigured(builder.Configuration);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseStaticFiles();

app.UseSwagger(c => {
    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "NAME");
});

app.MapEndpoints();

// app.Use((context, next) =>
// {
//     if (context.Request.Headers.TryGetValue("X-Forwarded-Path", out StringValues values) 
//         && values.Count > 0)
//     {
//         context.Request.PathBase = values[0];
//     }
//     return next();
// });

app.Run();

namespace LightsOn.WebApi
{
    public partial class Program { }
}