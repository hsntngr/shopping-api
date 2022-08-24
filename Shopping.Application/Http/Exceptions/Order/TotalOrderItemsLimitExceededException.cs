using Shopping.Application.Validations.Order;

namespace Shopping.Application.Http.Exceptions.Order;

public class TotalOrderItemsLimitExceededException : BusinessException
{
    public TotalOrderItemsLimitExceededException() : base($"You can not order more than {OrderValidations.MaxOrderItemsCount} products")
    {
    }
}