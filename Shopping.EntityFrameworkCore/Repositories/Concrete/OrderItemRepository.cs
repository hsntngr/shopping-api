using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Base.Concrete;

namespace Shopping.EntityFrameworkCore.Repositories.Concrete;

public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> CountOrderItemByOrderIdAsync(Guid orderId)
    {
        return await DbSet.CountAsync(x => x.OrderId == orderId);
    }

    public async Task<int> SumOrderItemTotalQuantityByOrderIdAsync(Guid orderId)
    {
        return await DbSet
            .Where(x => x.OrderId == orderId)
            .SumAsync(x => x.Quantity);
    }
}