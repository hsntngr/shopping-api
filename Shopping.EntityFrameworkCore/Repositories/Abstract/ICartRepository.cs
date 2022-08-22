using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Abstract;

public interface ICartRepository : IRepository<Cart>
{
    Task<ICollection<Cart>> GetCartItemsByUserId(Guid userId);
    Task<Cart?> GetCartById(Guid userId, Guid productId);
}