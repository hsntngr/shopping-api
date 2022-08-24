using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Abstract;

public interface IOrderRepository : IRepository<Order, Guid>
{
    Task<int> GetOrderCountOfToday(Guid userId);
    Task<Order?> GetOrderByCode(string code);
    Task<bool> CheckOrderExists(string code);
    Task<Order?> GetOrderWithOrderItems(Guid orderId);
    Task<OrderItem?> GetOrderItemById(Guid orderId, Guid productId);
}