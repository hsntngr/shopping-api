using Shopping.Domain.Entities;

namespace Shopping.Domain.Base.Abstract;

public interface ICreatableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public User Creator { get; set; }
}