using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Base;
using Shopping.Application.Resources.Order.OrderItem;
using Shopping.Application.Services.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Abstract;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/Orders/{orderId}/[controller]")]
public class OrderItemsController : ApplicationControllerBase
{
    private readonly IOrderItemService _orderItemService;


    public OrderItemsController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    /// <summary>
    /// Get List Of Order Items
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<ICollection<OrderItemResponse>> GetList(Guid orderId)
    {
        return await _orderItemService.GetOrderItems(orderId, CurrentUserId);
    }

    /// <summary>
    /// Increment quantity of the item
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("Increase")]
    public async Task<OrderItemResponse> IncrementQuantity(Guid orderId, IncrementOrderItemQuantityRequest request)
    {
        return await _orderItemService.IncreaseQuantity(orderId, request.ProductId, CurrentUserId);
    }

    /// <summary>
    /// Increment quantity of the item
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("Decrease")]
    public async Task<OrderItemResponse> DecrementQuantity(Guid orderId, DecrementOrderItemQuantityRequest request)
    {
        return await _orderItemService.DecreaseQuantity(orderId, request.ProductId, CurrentUserId);
    }

    /// <summary>
    /// Remove order item with all quantities
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    [Route("{productId}")]
    public async Task RemoveOrderItem(Guid orderId, Guid productId)
    {
        await _orderItemService.Remove(orderId, productId, CurrentUserId);
    }
}