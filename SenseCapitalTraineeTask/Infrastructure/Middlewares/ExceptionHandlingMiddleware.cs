using System.Text.Json;
using FluentValidation;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
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
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
    
    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            ValidationException => StatusCodes.Status422UnprocessableEntity,
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
        }

        return scError;
    }
}