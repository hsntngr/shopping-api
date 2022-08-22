namespace Shopping.Application.Http.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message = null) : base(message)
    {
    }
}