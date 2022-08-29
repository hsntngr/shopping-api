namespace Shopping.Application.Validations.Order;

public class OrderValidations
{
    
    public const int MaxOrderCountEachDay = 10;
    public class OrderItems
    {
        public const int MinCount = 1;
        public const int MaxCount = 10;
        public const int TotalQuantity = 10000;
    }

}