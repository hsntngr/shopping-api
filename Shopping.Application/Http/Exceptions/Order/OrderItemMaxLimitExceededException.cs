using Shopping.Application.Validations.Order;

namespace Shopping.Application.Http.Exceptions.Order;

public class OrderItemMaxLimitExceededException : BusinessException
{
    public OrderItemMaxLimitExceededException() : base($"You can not order more than {OrderValidations.OrderItems.MaxCount} for each products")
    {
    }
}