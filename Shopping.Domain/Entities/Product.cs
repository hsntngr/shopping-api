using Shopping.Domain.Base;
using Shopping.Domain.Base.Abstract;
using Shopping.Domain.Base.Concrete;

namespace Shopping.Domain.Entities;

public class Product : FullAuditedAggregateRootEntity<Guid>
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public virtual ICollection<Cart> CartItems { get; set; }

    public Product()
    {
    }

    public Product(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}