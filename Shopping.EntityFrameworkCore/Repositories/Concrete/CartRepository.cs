using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Contexts;
using Shopping.EntityFrameworkCore.Repositories.Abstract;
using Shopping.EntityFrameworkCore.Repositories.Base.Concrete;

namespace Shopping.EntityFrameworkCore.Repositories.Concrete;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Cart>> GetCartItemsByUserId(Guid userId)
    {
        return await DbSet
            .Where(x => x.UserId == userId)
            .Include(x => x.Product)
            .ToListAsync();
    }

    public async Task<ICollection<Cart>> GetCartItemsByUserId(Guid userId, ICollection<Guid> productIds)
    {
        return await DbSet
            .Where(x => x.UserId == userId && productIds.Contains(x.ProductId))
            .Include(x => x.Product)
            .ToListAsync();
    }

    public async Task<Cart?> GetCartById(Guid userId, Guid productId)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task<Cart?> GetCartWithDetailsById(Guid userId, Guid productId)
    {
        return await DbSet
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task<int> CountTotalCartItemsByUserId(Guid userId, ICollection<Guid> productIds)
    {
        return await DbSet
            .Where(x => x.UserId == userId && productIds.Contains(x.ProductId))
            .SumAsync(x => x.Quantity);
    }

    public async Task<Cart?> GetCartItemHasMaxQuantity(Guid userId, ICollection<Guid> productIds)
    {
        return await DbSet
            .Where(x => x.UserId == userId && productIds.Contains(x.ProductId))
            .OrderByDescending(x => x.Quantity).FirstOrDefaultAsync();
    }
}