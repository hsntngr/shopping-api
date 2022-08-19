using Shopping.Domain.Base.Abstract;

namespace Shopping.Domain.Base.Concrete;

public abstract class Entity : IEntity
{
    public abstract object[] GetCompositeKeys();
}

public abstract class Entity<TKeyType> : IEntity<TKeyType>
{
    public TKeyType Id { get; set; }
}