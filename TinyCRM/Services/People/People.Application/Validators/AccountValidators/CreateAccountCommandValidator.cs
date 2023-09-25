using FluentValidation;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Domain.Constants;

namespace People.Application.Validators.AccountValidators;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(account => account.Email)
            .EmailAddress()
            .When(model => !string.IsNullOrWhiteSpace(model.Email));

        RuleFor(p => p.Phone)
            .MinimumLength(10)
            .MaximumLength(30)
            .Matches(RegexPatterns.PhoneNumber)
            .When(model => !string.IsNullOrWhiteSpace(model.Phone))
            ;

        RuleFor(account => account.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(account => account.Address)
            .MinimumLength(2)
            .MaximumLength(255)
            .When(model => !string.IsNullOrWhiteSpace(model.Address))
            ;
    }
}