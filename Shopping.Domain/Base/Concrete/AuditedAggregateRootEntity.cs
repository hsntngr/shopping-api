using Shopping.Domain.Base.Abstract;
using Shopping.Domain.Entities;

namespace Shopping.Domain.Base.Concrete;

public abstract class AuditedAggregateRootEntity : AggregateRootEntity, ICreatableEntity, IEditableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public virtual User Creator { get; set; }

    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public virtual User Modifier { get; set; }
}

public abstract class AuditedAggregateRootEntity<TKeyType> : AggregateRootEntity<TKeyType>, ICreatableEntity, IEditableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public virtual User Creator { get; set; }


    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public virtual User Modifier { get; set; }
}