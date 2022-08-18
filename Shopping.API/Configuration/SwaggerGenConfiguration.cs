using Microsoft.OpenApi.Models;

namespace Shopping.API.Configuration;

public static class SwaggerGenConfiguration
{
    public static void AddSwaggerGenConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Shopping Assessment API",
                Description = "Basic Restful Application To Purchase Products - Domain Driven Design - .Net Core - Ef Core - PostgresSQL",
                Contact = new OpenApiContact
                {
                    Name = "Hasan Teoman TINGIR",
                    Email = "teoman.tingir@gmail.com"
                }
            });
        });
    }
}