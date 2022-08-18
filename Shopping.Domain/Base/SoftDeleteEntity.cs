namespace Shopping.Domain.Base;

public abstract class SoftDeleteEntity : Entity
{
    public DateTime DeletedAt { get; set; }
    public DateTime DeletedBy { get; set; }
}

public abstract class SoftDeleteEntity<TKeyType> : Entity<TKeyType>
{
    public DateTime DeletedAt { get; set; }
    public DateTime DeletedBy { get; set; }
}