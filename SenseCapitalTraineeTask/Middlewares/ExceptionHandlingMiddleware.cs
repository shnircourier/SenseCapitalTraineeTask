using System.Text.Json;
using BusinessLogic.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace SenseCapitalTraineeTask.Middlewares;

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
        var response = new
        {
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
    
    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    
    private static List<ExceptionModel> GetErrors(Exception exception)
    {
        List<ExceptionModel> errors = null;
        switch (exception)
        {
            case ValidationException validationException:
                errors = validationException.Errors.Select(e => new ExceptionModel
                {
                    ErrorCode = e.ErrorCode,
                    ErrorMessage = e.ErrorMessage
                }).ToList();
                break;
            case NotFoundException notFoundException:
                var errorList = new List<ExceptionModel>();
                errorList.Add(new ExceptionModel
                {
                    ErrorCode = "NotFoundException",
                    ErrorMessage = notFoundException.Message
                });
                break;
        }

        return errors;
    }
}