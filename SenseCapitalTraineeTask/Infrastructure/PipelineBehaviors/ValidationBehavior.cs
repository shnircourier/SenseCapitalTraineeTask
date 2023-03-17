using FluentValidation;
using MediatR;

namespace SenseCapitalTraineeTask.Infrastructure.PipelineBehaviors;

/// <summary>
/// Обработчик валидации данных
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validators">Все модели валидации</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(x => x.ValidateAsync(context, cancellationToken))
            .SelectMany(x => x.Result.Errors)
            .Where(x => x != null)
            .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        
        return await next();
    }
}