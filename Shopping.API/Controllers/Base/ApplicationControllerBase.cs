using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Resources.Product;
using Shopping.Domain.Entities;

namespace Shopping.API.Controllers.Base;

public class ApplicationControllerBase : ControllerBase
{
    protected User? CurrentUser;
    protected Guid CurrentUserId = Guid.Empty;

    public ApplicationControllerBase()
    {
        if (HttpContext?.Items?.ContainsKey("CurrentUserId") == true)
        {
            CurrentUserId = Guid.Parse((string) HttpContext.Items["CurrentUserId"]!);
            CurrentUser = (User) HttpContext.Items["CurrentUser"]!;
        }
    }
}