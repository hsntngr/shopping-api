namespace Shopping.Application.Http.Response;

public class BadRequestResponse
{
    public string Message { get; set; }
    public BadRequestResponse(string message)
    {
        Message = message;
    }

}