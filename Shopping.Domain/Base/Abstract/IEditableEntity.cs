using Shopping.Domain.Entities;

namespace Shopping.Domain.Base.Abstract;

public interface IEditableEntity
{
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }

    public User Modifier { get; set; }
}