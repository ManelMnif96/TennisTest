using System.Net;
using System.Text.Json;
using AtelierTest.Exceptions;

namespace AtelierTest.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        HttpStatusCode status = HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred.";

        if (ex is PlayerNotFoundException)
        {
            status = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else if (ex is EmptyPlayersException)
        {
            status = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else if (ex is InvalidPlayerDataException)
        {
            status = HttpStatusCode.BadRequest;
            message = ex.Message;
        }

        var response = new
        {
            error = message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}