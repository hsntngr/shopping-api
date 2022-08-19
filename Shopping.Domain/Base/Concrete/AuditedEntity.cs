namespace Shopping.Domain.Base.Concrete;

public abstract class AuditedEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public abstract override object[] GetCompositeKeys();
}

public abstract class AuditedEntity<TKeyType> :  Entity<TKeyType>
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
}