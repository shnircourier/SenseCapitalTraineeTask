using FluentValidation;
using MediatR;

namespace BusinessLogic.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

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