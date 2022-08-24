namespace Shopping.Application.Resources.Order.OrderItem;

public class OrderItemResponse
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public ushort Quantity { get; set; }
}