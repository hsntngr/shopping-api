using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Base.Concrete;

namespace Shopping.EntityFrameworkCore.Repositories.Concrete;

public class OrderRepository : Repository<Order, Guid>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> GetOrderCountOfToday(Guid userId)
    {
        DateTime date = DateTime.Now;
        DateTime startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        return await DbSet.CountAsync(x => x.UserId == userId && x.CreatedAt >= startOfDay);
    }

    public async Task<Order?> GetOrderByCode(string code)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<bool> CheckOrderExists(string code)
    {
        return await DbSet.AnyAsync(x => x.Code == code);
    }

    public async Task<Order?> GetOrderWithOrderItems(Guid orderId)
    {
        return await DbSet.Where(x => x.Id == orderId)
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync();
    }

    public async Task<OrderItem?> GetOrderItemById(Guid orderId, Guid productId)
    {
        return await DbSet.Where(x => x.Id == orderId)
            .Select(x => x.OrderItems.FirstOrDefault(i => i.ProductId == productId))
            .FirstOrDefaultAsync();
    }
}