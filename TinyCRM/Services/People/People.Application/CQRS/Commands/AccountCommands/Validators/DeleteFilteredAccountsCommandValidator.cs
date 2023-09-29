using FluentValidation;
using People.Application.CQRS.Commands.AccountCommands.Requests;

namespace People.Application.CQRS.Commands.AccountCommands.Validators;

public class DeleteFilteredAccountsCommandValidator : AbstractValidator<DeleteFilteredAccountsCommand>
{
    public DeleteFilteredAccountsCommandValidator()
    {
        RuleFor(account => account.Keyword)
            .MaximumLength(255);
    }
}