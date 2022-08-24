using Shopping.Domain.Entities;
using Shopping.EntityFrameworkCore.Repositories.Base.Abstract;

namespace Shopping.EntityFrameworkCore.Repositories.Abstract;

public interface IOrderItemRepository : IRepository<OrderItem>
{
}