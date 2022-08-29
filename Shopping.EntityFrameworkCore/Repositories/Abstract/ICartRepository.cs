using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Abstract;

public interface ICartRepository : IRepository<Cart>
{
    Task<ICollection<Cart>> GetCartItemsByUserId(Guid userId);
    Task<ICollection<Cart>> GetCartItemsByUserId(Guid userId, ICollection<Guid> productIds);
    Task<Cart?> GetCartById(Guid userId, Guid productId);
    Task<Cart?> GetCartWithDetailsById(Guid userId, Guid productId);
    Task<int> CountTotalCartItemsByUserId(Guid userId, ICollection<Guid> productIds);
    Task<Cart?> GetCartItemHasMaxQuantity(Guid userId, ICollection<Guid> productIds);
}