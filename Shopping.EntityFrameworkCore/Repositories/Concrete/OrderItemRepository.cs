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
}