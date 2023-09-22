using FluentValidation;
using People.Application.CQRS.Commands.Requests;
using People.Domain.Constants;

namespace People.Application.Validations;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(account => account.Email)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Email))
            .EmailAddress();

        RuleFor(p => p.Phone)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Phone))
            .MinimumLength(10)
            .MaximumLength(30)
            .Matches(RegexPatterns.PhoneNumber)
            ;

        RuleFor(account => account.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(account => account.Address)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Address))
            .MinimumLength(2)
            .MaximumLength(255);
    }
}