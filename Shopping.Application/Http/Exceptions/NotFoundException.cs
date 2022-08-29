namespace Shopping.Application.Http.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message = "Requested resource not found on the server") : base(message)
    {
    }
}