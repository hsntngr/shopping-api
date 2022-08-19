using Shopping.Domain.Base.Abstract;
using Shopping.Domain.Entities;

namespace Shopping.Domain.Base.Concrete;

public abstract class FullAuditedAggregateRootEntity : AuditedAggregateRootEntity, ISoftDeleteEntity
{
    public DateTime DeletedAt { get; set; }
    public Guid DeletedBy { get; set; }
    public virtual User Deleter { get; set; }
    public abstract override object[] GetCompositeKeys();
}

public abstract class FullAuditedAggregateRootEntity<TKeyType> : AuditedAggregateRootEntity<TKeyType>, ISoftDeleteEntity
{
    public DateTime DeletedAt { get; set; }
    public Guid DeletedBy { get; set; }
    public virtual User Deleter { get; set; }
}