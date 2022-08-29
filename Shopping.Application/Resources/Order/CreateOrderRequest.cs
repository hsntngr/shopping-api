namespace Shopping.Application.Resources.Order;

public class CreateOrderRequest
{
    public ICollection<Guid> ProductIds { get; set; }
}