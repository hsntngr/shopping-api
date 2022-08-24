namespace Shopping.Application.Http.Exceptions.Order;

public class SubmittedOrderCanNotEditException : Exception
{
    public SubmittedOrderCanNotEditException() : base("Submitted order can not be edited")
    {
        
    }
}