namespace Shopping.Application.Http.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("You do not permission to perform this action")
    {
        
    }
}