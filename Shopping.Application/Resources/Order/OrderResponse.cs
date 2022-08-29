using Shopping.Application.Resources.Order.OrderItem;
using Shopping.Domain.Shared.Enums;

namespace Shopping.Application.Resources.Order;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Code { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<OrderItemResponse> OrderItems { get; set; }

    public OrderResponse()
    {
        OrderItems = new List<OrderItemResponse>();
    }
}