using Shopping.Application.Validations.Order;

namespace Shopping.Application.Http.Exceptions.Order;

public class MinimumOrderItemsNotSatisfiedException : BusinessException
{
    public MinimumOrderItemsNotSatisfiedException() : base($"You muse have at least {OrderValidations.OrderItems.MinCount} product in your order!")
    {
    }
}