using Shopping.Domain.Entities;

namespace Shopping.Domain.Base.Abstract;

public interface ISoftDeleteEntity
{
    public DateTime DeletedAt { get; set; }
    public Guid DeletedBy { get; set; }

    public User Deleter { get; set; }
}