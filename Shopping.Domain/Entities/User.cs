using Shopping.Domain.Base;
using Shopping.Domain.Base.Concrete;

namespace Shopping.Domain.Entities;

public class User : FullAuditedAggregateRootEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Cart> CartItems { get; set; }
}