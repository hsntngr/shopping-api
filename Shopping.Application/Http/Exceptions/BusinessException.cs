namespace Shopping.Application.Http.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
        
    }
}