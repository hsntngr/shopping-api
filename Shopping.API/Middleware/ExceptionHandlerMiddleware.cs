using System.Text.Json;
using Shopping.Application.Http.Exceptions;
using Shopping.Application.Http.Response;

namespace Shopping.API.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;

                response.StatusCode = StatusCodes.Status200OK;
                response.ContentType = "application/json";
                BadRequestResponse apiResponse;

                switch (exception)
                {
                    case UnauthorizedAccessException ex:
                        context.Response.StatusCode = 403;
                        apiResponse = new BadRequestResponse(ex.Message);
                        break;
                    case NotFoundException ex:
                        context.Response.StatusCode = 404;
                        apiResponse = new BadRequestResponse(ex.Message);
                        break;
                    case BusinessException ex:
                        context.Response.StatusCode = 420;
                        apiResponse = new BadRequestResponse(ex.Message);
                        break;
                    default: throw;
                }

                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                await response.WriteAsync(JsonSerializer.Serialize(apiResponse, jsonSerializerOptions));
            }
        }
    }
}