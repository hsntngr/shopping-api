using Shopping.Domain.Base;

namespace Shopping.Domain.Entities;

public class Product : FullAuditedAggregateRootEntity<Guid>
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}