namespace Shopping.Domain.Base.Abstract;

public interface IEntity
{
    object[] GetCompositeKeys();
}

public interface IEntity<TKeyType>
{
    public TKeyType Id { get; set; }
}