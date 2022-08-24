namespace Shopping.Application.Http.Exceptions.Order;

public class OrderItemMinQuantityException : Exception
{
    public OrderItemMinQuantityException() : base("Order item quantity can not be lower than 1")
    {
        
    }
}