using Shopping.Application.Resources.Order.OrderItem;

namespace Shopping.Application.Services.Abstract;

public interface IOrderItemService
{
    Task<ICollection<OrderItemResponse>> GetOrderItems(Guid orderId, Guid userId);
    Task<OrderItemResponse> IncreaseQuantity(Guid orderId, Guid productId, Guid userId);
    Task<OrderItemResponse> DecreaseQuantity(Guid orderId, Guid productId, Guid userId);
    Task Remove(Guid orderId, Guid productId, Guid userId);
}