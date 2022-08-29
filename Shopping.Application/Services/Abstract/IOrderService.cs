using Shopping.Application.Resources.Order;
using Shopping.Application.Resources.Order.OrderItem;

namespace Shopping.Application.Services.Abstract;

public interface IOrderService
{
    Task<ICollection<OrderResponse>> GetList(Guid userId);
    Task<OrderResponse> CreateOrder(CreateOrderRequest createOrderRequest, Guid userId);
    Task<OrderResponse> CompleteOrder(Guid orderId, Guid userId);
    Task<string> GenerateUniqueOrderCodeAsync();
}