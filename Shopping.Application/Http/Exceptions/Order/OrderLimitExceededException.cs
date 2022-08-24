using Shopping.Application.Validations.Order;

namespace Shopping.Application.Http.Exceptions.Order;

public class OrderLimitExceededException : BusinessException
{
    public OrderLimitExceededException() : base($"You can not order more than {OrderValidations.MaxOrderCountEachDay} times a day")
    {
    }
}