using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shopping.EntityFrameworkCore.Contexts;

namespace Shopping.API.Configuration;

public static class ConnectionConfiguration
{
    public static void AddConnectionConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder => builder.UseNpgsql(configuration.GetConnectionString("Default")));
    }
}