namespace Shopping.API.Configuration;

public static class CrossOriginConfiguration
{
    public static void AddCrossOriginConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "Default",
                policy =>
                {
                    policy.WithOrigins(configuration.GetValue<string>("AllowedOrigins").Split(','))
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}