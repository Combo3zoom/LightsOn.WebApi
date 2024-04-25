using LightsOn.Application;
using LightsOn.WebApi;
using LightsOn.WebApi.Infrastructure;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyVaultIfConfigured(builder.Configuration);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7239") // Set the allowed origin here
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}
app.UseForwardedHeaders();

app.UseHealthChecks("/health");
app.UseStaticFiles();

app.UseSwagger(options =>
{
    options.RouteTemplate = "api/swagger/{documentName}/swagger.{json|yaml}";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NAME");
    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "NAME Reverse proxy");
});

app.UseCors("AllowSpecificOrigin");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/swagger"));

app.MapEndpoints();

app.Run();

namespace LightsOn.WebApi
{
    public partial class Program { }
}