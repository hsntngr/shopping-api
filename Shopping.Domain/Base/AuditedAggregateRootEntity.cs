namespace Shopping.Domain.Base;

public abstract class AuditedAggregateRootEntity : AggregateRootEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
}

public abstract class AuditedAggregateRootEntity<TKeyType> : AggregateRootEntity<TKeyType>
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
}