using Shopping.Application.Validations.Order;

namespace Shopping.Application.Http.Exceptions.Order;

public class OrderItemLimitExceededException : BusinessException
{
    public OrderItemLimitExceededException() : base($"You can not order more than {OrderValidations.OrderItems.MaxCount} for each products")
    {
    }
}