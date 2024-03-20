using System.Data.Common;
using LightsOn.Application.Common.Interfaces;
using LightsOn.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LightsOn.Application.IntegrationTests;

using static Testing;

public class CustomWebApplicationFactory : WebApplicationFactory<WebApi.Program>
{
    private readonly DbConnection _connection;
    private readonly Mock<IDateTimeOffSet> _iDateTimeOffset;

    public CustomWebApplicationFactory(DbConnection connection, Mock<IDateTimeOffSet> iDateTimeOffset)
    {
        _connection = connection;
        _iDateTimeOffset = iDateTimeOffset;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<IUser>()
                .AddTransient(provider => Mock.Of<IUser>(s => s.Id == GetUserId()));
            services
                .RemoveAll<IDateTimeOffSet>();
            services
                .AddScoped<IDateTimeOffSet>(provider => _iDateTimeOffset.Object);
            services
                .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                    options.UseSqlServer(_connection);
                });
        });
    }
}
