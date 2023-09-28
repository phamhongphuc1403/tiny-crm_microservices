using FluentValidation;
using People.Application.CQRS.Commands.ContactCommands.Requests;
using People.Domain.Constants;

namespace People.Application.CQRS.Commands.ContactCommands.Validators;

public class EditContactCommandValidator : AbstractValidator<EditContactCommand>
{
    public EditContactCommandValidator()
    {
        RuleFor(contact => contact.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("'{PropertyValue}' is not a valid email address.")
            ;

        RuleFor(p => p.Phone)
            .MinimumLength(10)
            .MaximumLength(30)
            .Matches(RegexPatterns.PhoneNumber)
            .WithMessage("'{PropertyValue}' is not a valid phone number.")
            .When(model => !string.IsNullOrWhiteSpace(model.Phone))
            ;

        RuleFor(contact => contact.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}