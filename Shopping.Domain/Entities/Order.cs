using Shopping.Domain.Base.Concrete;
using Shopping.Domain.Shared.Enums;

namespace Shopping.Domain.Entities;

public class Order : AuditedAggregateRootEntity<Guid>
{
    public Guid UserId { get; set; }
    public string Code { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}