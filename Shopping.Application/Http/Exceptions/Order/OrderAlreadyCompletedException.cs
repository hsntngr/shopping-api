namespace Shopping.Application.Http.Exceptions.Order;

public class OrderAlreadyCompletedException : Exception
{
    public OrderAlreadyCompletedException() : base("Order already completed!")
    {
        
    }
}