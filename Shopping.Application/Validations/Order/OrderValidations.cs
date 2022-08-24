namespace Shopping.Application.Validations.Order;

public class OrderValidations
{
    
    public const int MinOrderItemsCount = 1;
    public const int MaxOrderItemsCount = 10000;
    public const int MaxOrderCountEachDay = 10;
    public class OrderItems
    {
        public const int MaxCount = 10;
    }

}