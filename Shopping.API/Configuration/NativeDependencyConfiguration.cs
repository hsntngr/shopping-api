using Microsoft.Extensions.DependencyInjection.Extensions;
using Shopping.API.Middleware;
using Shopping.Application.Services.Abstract;
using Shopping.Application.Services.Concrete;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Concrete;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Concrete;

namespace Shopping.API.Configuration;

public static class NativeDependencyConfiguration
{
    public static void AddNativeDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IUserService, UserService>();
        services.AddTransient<ExceptionHandlerMiddleware>();
        services.AddTransient<CurrentUserMiddleware>();
    }
}