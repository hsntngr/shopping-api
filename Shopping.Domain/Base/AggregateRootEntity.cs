namespace Shopping.Domain.Base;

public abstract class AggregateRootEntity : Entity
{
}
public abstract class AggregateRootEntity<TKeyType> : Entity<TKeyType>
{
}