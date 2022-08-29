using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Base;
using Shopping.Application.Resources.Cart;
using Shopping.Application.Services.Abstract;

namespace Shopping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ApplicationControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    /// Get cart items belongs to user
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<ICollection<CartItemResponse>> GetList()
    {
        return await _cartService.GetCartItemsByUserId(CurrentUserId);
    }

    /// <summary>
    /// Add new cart item or increment the quantity of the existing item
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("CreateOrIncrement")]
    public async Task<CartItemResponse> Create(CreateCartItemRequest request)
    {
        return await _cartService.CreateCartItem(request, CurrentUserId);
    }

    /// <summary>
    /// Decrement quantity of the cart item
    /// </summary>
    /// <param name="request"></param>
    [Authorize]
    [HttpPut("RemoveOrDecrement")]
    public async Task Remove(RemoveCartItemRequest request)
    {
        await _cartService.RemoveCartItem(request, CurrentUserId);
    }

    /// <summary>
    /// Remove all cart items 
    /// </summary>
    [Authorize]
    [HttpDelete]
    public async Task RemoveAll()
    {
        await _cartService.RemoveAllCartItems(CurrentUserId);
    }
}