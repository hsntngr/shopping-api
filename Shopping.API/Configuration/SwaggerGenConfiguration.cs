using System.Reflection;
using Microsoft.OpenApi.Models;
using Shopping.API.Filters;

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
                Description = "Basic Restful API To Purchase Products - Domain Driven Design - .Net Core - Ef Core - PostgresSQL",
                Contact = new OpenApiContact
                {
                    Name = "Hasan Teoman TINGIR",
                    Email = "teoman.tingir@gmail.com"
                }
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer eyJhbGciOiJSUzI..')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}