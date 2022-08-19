namespace Shopping.Domain.Base.Concrete;

public abstract class AggregateRootEntity : Entity
{
}
public abstract class AggregateRootEntity<TKeyType> : Entity<TKeyType>
{
}