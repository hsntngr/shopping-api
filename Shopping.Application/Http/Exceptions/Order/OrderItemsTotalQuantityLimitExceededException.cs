using Shopping.Application.Validations.Order;

namespace Shopping.Application.Http.Exceptions.Order;

public class OrderItemsTotalQuantityLimitExceededException : BusinessException
{
    public OrderItemsTotalQuantityLimitExceededException() : base($"You can not order more than {OrderValidations.OrderItems.TotalQuantity} products at a time")
    {
    }
}