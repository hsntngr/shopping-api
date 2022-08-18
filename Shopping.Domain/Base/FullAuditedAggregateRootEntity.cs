namespace Shopping.Domain.Base;

public class FullAuditedAggregateRootEntity : AuditedAggregateRootEntity
{
    public DateTime DeletedAt { get; set; }
    public DateTime DeletedBy { get; set; }
}

public class FullAuditedAggregateRootEntity<TKeyType> : AuditedAggregateRootEntity<TKeyType>
{
    public DateTime DeletedAt { get; set; }
    public DateTime DeletedBy { get; set; }
}