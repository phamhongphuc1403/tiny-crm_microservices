using FluentValidation;
using Sales.Application.CQRS.Commands.AccountCommands.Requests;

namespace Sales.Application.Validators.AccountValidators;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(account => account.Email)
            .EmailAddress()
            .When(model => !string.IsNullOrWhiteSpace(model.Email))
            .WithMessage("'{PropertyValue}' is not a valid email address.");

        RuleFor(account => account.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}