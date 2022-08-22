using Shopping.Application.Resources.Cart;

namespace Shopping.Application.Services.Abstract;

public interface ICartService
{
    Task<ICollection<CartItemResponse>> GetCartItemsByUserId(Guid userId);
    Task<CartItemResponse> CreateCartItem(CreateCartItemRequest request, Guid userId);
    Task RemoveCartItem(RemoveCartItemRequest request, Guid userId);
    Task RemoveAllCartItems(Guid userId);
}