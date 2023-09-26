using FluentValidation;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Domain.Constants;

namespace People.Application.Validators.AccountValidators;

public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
{
    public EditAccountCommandValidator()
    {
        RuleFor(account => account.Email)
            .EmailAddress()
            .When(model => !string.IsNullOrWhiteSpace(model.Email))
            .WithMessage("'{PropertyValue}' is not a valid email address.")
            ;

        RuleFor(p => p.Phone)
            .MinimumLength(10)
            .MaximumLength(30)
            .Matches(RegexPatterns.PhoneNumber)
            .When(model => !string.IsNullOrWhiteSpace(model.Phone))
            ;

        RuleFor(account => account.Name)
            .NotEmpty()
            .MaximumLength(255)
            .When(model => !string.IsNullOrWhiteSpace(model.Name))
            ;

        RuleFor(account => account.Address)
            .MinimumLength(2)
            .MaximumLength(255)
            .When(model => !string.IsNullOrWhiteSpace(model.Address))
            ;
    }
}