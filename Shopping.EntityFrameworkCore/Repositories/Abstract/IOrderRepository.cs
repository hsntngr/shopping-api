using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Abstract;

public interface IOrderRepository : IRepository<Order, Guid>
{
    Task<int> GetOrderCountOfTodayAsync(Guid userId);
    Task<Order?> GetOrderByCodeAsync(string code);
    Task<bool> CheckOrderExistsAsync(string code);
    Task<Order?> GetOrderWithOrderItemsAsync(Guid orderId);
    Task<OrderItem?> GetOrderItemByIdAsync(Guid orderId, Guid productId);
    Task<ICollection<Order>> GetUserOrdersWithDetailsAsync(Guid userId);
}