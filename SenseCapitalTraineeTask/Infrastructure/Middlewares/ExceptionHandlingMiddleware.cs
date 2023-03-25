using System.Text.Json;
using FluentValidation;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

// ReSharper disable once IdentifierTypo
namespace SenseCapitalTraineeTask.Infrastructure.Middlewares;

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
            HttpRequestException => StatusCodes.Status503ServiceUnavailable,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            ScException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    
    private static ScError GetErrors(Exception exception)
    {
        var scError = new ScError();
        
        switch (exception)
        {
            case ValidationException validationException:
                scError.Message = validationException.Message;
                scError.ModelState = validationException.Errors
                    .ToDictionary(
                        x => x.PropertyName,
                        x => new List<string>(new[] { x.ErrorMessage }));
                break;
            case ScException scException:
                scError.Message = scException.Message;
                break;
            case FormatException:
                scError.Message = "Некорректный формат Id. Необходимо 24 символа(0-9, a-f)";
                break;
            case HttpRequestException:
                scError.Message = "Не удалось соединиться с сервисом";
                break;
        }

        return scError;
    }
}