using Shopping.Application.Resources.Product;

namespace Shopping.Application.Resources.Cart;

public class CartItemResponse
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public ushort Quantity { get; set; }
    public ProductResponse Product { get; set; }
}