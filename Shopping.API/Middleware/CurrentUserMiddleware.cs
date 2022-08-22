using System.Security.Claims;
using Shopping.EntityFrameworkCore.Repositories.Abstract;

namespace Shopping.API.Middleware;

public class CurrentUserMiddleware : IMiddleware
{
    private readonly IUserRepository _userRepository;

    public CurrentUserMiddleware(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var claim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim != null)
        {
            context.Items["CurrentUserId"] = claim.Value;
            context.Items["CurrentUser"] = await _userRepository.GetById(Guid.Parse(claim.Value));
        }

        await next.Invoke(context);
    }
}