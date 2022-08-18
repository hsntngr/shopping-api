using Shopping.Domain.Base;

namespace Shopping.Domain.Entities;

public class Cart : AuditedEntity
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public ushort Quantity { get; set; }

    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}