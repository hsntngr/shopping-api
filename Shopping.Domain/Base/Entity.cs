namespace Shopping.Domain.Base;

public abstract class Entity
{
}

public abstract class Entity<TKeyType>
{
    public TKeyType Id { get; set; }
}