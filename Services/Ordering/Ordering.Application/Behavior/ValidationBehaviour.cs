using FluentValidation;
using MediatR;

namespace Ordering.Application.Behavior;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validators.Any())
        {
            var content = new ValidationContext<TRequest>(request);

            //Runs validations One by One, and return result

            var validationResults=await Task.WhenAll(_validators.Select(v=>v.ValidateAsync(content, cancellationToken)));

            //Check for Failures
            var failures = validationResults.SelectMany(e => e.Errors).Where(f => f is not null).ToList();
            if (failures.Any()) throw new ValidationException(failures);
        }

        //On success, cotinue with Mediator Pipeline
        return await next();
    }
}
