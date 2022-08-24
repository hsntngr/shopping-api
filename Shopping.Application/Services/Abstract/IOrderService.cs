using Shopping.Application.Resources.Order;

namespace Shopping.Application.Services.Abstract;

public interface IOrderService
{
    Task<ICollection<OrderResponse>> GetList(Guid userId);
    Task<OrderResponse> Create(Guid userId);
    Task<string> GenerateUniqueOrderCodeAsync();
}