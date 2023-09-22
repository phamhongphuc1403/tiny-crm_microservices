using System.Text.RegularExpressions;
using FluentValidation;
using People.Application.CQRS.Commands.Requests;

namespace People.Application.Validations;

public partial class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
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
            .Matches(MyRegex())
            ;

        RuleFor(account => account.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(account => account.Address)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Address))
            .MinimumLength(2)
            .MaximumLength(255);
    }

    [GeneratedRegex(@"(84|0[3|5|7|8|9])+([0-9]{8})\b")]
    private static partial Regex MyRegex();
}