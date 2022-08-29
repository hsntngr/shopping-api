using Shopping.Application.Validations.Order;

namespace Shopping.Application.Http.Exceptions.Cart;

public class CartItemMaxLimitExceededException : BusinessException
{
    public CartItemMaxLimitExceededException() : base($"You can not add more than {OrderValidations.OrderItems.MaxCount} to cart for each product")
    {
    }
}