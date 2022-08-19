using Shopping.Domain.Base;
using Shopping.Domain.Base.Concrete;

namespace Shopping.Domain.Entities;

public class Cart : AuditedEntity
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public ushort Quantity { get; set; }

    public virtual User User { get; set; }
    public virtual Product Product { get; set; }

    public override object[] GetCompositeKeys()
    {
        return new object[] {ProductId, UserId};
    }
}