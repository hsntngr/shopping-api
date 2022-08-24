using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Base;
using Shopping.Application.Resources.Order;
using Shopping.Application.Services.Abstract;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ApplicationControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    /// <summary>
    /// Get List Of Orders Belongs to Authenticated User
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<ICollection<OrderResponse>> GetList()
    {
        return await _orderService.GetList(CurrentUserId);
    }
    
    /// <summary>
    /// Create New Order From Cart Items
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<OrderResponse> Create()
    {
        return await _orderService.Create(CurrentUserId);
    }
}