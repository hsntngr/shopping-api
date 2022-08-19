using Shopping.Domain.Base;
using Shopping.Domain.Base.Concrete;

namespace Shopping.Domain.Entities;

public class OrderItem : Entity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public ushort Quantity { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
    public override object[] GetCompositeKeys()
    {
        return new object[] { ProductId, OrderId };
    }
}