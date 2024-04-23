using LightsOn.Application;
using LightsOn.WebApi;
using LightsOn.WebApi.Infrastructure;

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

app.UseHealthChecks("/health");
// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
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