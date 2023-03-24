using System.Text.Json;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Rooms.Infrastructure;

/// <summary>
/// Обработчик исключений
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await HandleExceptionAsync(context, e);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var getScError = GetErrors(exception);
        var response = new ScResult
        {
            Error = getScError
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
    
    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            FormatException => StatusCodes.Status422UnprocessableEntity,
            ScException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    
    private static ScError GetErrors(Exception exception)
    {
        var scError = new ScError();

        scError.Message = exception switch
        {
            ScException scException => scException.Message,
            FormatException => "Некорректный формат Id. Необходимо 24 символа(0-9, a-f)",
            _ => scError.Message
        };

        return scError;
    }
}