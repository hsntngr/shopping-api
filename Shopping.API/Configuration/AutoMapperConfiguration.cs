using System.Reflection;

namespace Shopping.API.Configuration;

public static class AutoMapperConfiguration
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.Load("Shopping.Application"));
    }
}