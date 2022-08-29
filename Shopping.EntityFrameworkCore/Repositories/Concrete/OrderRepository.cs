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

    public async Task<int> GetOrderCountOfTodayAsync(Guid userId)
    {
        DateTime date = DateTime.UtcNow;
        DateTime startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, DateTimeKind.Utc);
        return await DbSet.CountAsync(x => x.UserId == userId && x.CreatedAt >= startOfDay);
    }

    public async Task<Order?> GetOrderByCodeAsync(string code)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<bool> CheckOrderExistsAsync(string code)
    {
        return await DbSet.AnyAsync(x => x.Code == code);
    }

    public async Task<Order?> GetOrderWithOrderItemsAsync(Guid orderId)
    {
        return await DbSet.Where(x => x.Id == orderId)
            .Include(x => x.OrderItems.OrderBy(x => x.Product.Name))
            .ThenInclude(x => x.Product)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<OrderItem?> GetOrderItemByIdAsync(Guid orderId, Guid productId)
    {
        return await DbSet.Where(x => x.Id == orderId)
            .Select(x => x.OrderItems.FirstOrDefault(i => i.ProductId == productId))
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<Order>> GetUserOrdersWithDetailsAsync(Guid userId)
    {
        return await DbSet
            .Where(x => x.UserId == userId)
            .Include(x => x.OrderItems.OrderBy(x => x.Product.Name))
            .ThenInclude(x => x.Product)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}