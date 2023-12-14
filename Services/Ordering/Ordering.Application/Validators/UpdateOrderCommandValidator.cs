using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(r => r.UserName).NotEmpty().WithMessage("{UserName} is required").NotNull().MaximumLength(70).WithMessage("{UserName} cannot exceed 70 characters");

        RuleFor(r => r.TotalPrice).GreaterThan(-1).WithMessage("{TotalPrice} should not be negative");
        RuleFor(r => r.EmailAddress).NotEmpty().WithMessage("{EmailAddress} is required");
        RuleFor(r => r.FirstName).NotEmpty().NotNull().WithMessage("{FirstName} is required");
        RuleFor(r => r.LastName).NotEmpty().NotNull().WithMessage("{LastName} is required");
    }
}
