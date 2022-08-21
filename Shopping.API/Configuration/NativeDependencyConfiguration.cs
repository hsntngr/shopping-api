using Shopping.Application.Abstract;
using Shopping.Application.Concrete;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Base.Concrete;
using Shopping.EntityFrameworkCore.Repositories.Concrete;
using Shopping.EntityFrameworkCore.UnitOfWork.Abstract;
using Shopping.EntityFrameworkCore.UnitOfWork.Concrete;

namespace Shopping.API.Configuration;

public static class NativeDependencyConfiguration
{
    public static void AddNativeDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}