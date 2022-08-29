using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopping.Domain.Entities;

namespace Shopping.API.Controllers.Base;

public class ApplicationControllerBase : Controller
{
    protected User? CurrentUser;
    protected Guid CurrentUserId = Guid.Empty;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Items.ContainsKey("CurrentUserId"))
        {
            CurrentUserId = Guid.Parse((string) HttpContext.Items["CurrentUserId"]!);
            CurrentUser = (User) HttpContext.Items["CurrentUser"]!;
        }

        base.OnActionExecuting(context);
    }
}