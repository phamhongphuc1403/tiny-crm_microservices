using FluentValidation;
using Sales.Application.CQRS.Commands.AccountCommands.Requests;

namespace Sales.Application.Validators.AccountValidators;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(account => account.Email)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Email))
            .EmailAddress();

        RuleFor(account => account.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}