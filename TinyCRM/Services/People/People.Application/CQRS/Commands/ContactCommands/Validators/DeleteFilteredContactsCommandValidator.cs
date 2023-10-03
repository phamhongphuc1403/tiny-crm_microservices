using FluentValidation;
using People.Application.CQRS.Commands.ContactCommands.Requests;

namespace People.Application.CQRS.Commands.ContactCommands.Validators;

public class DeleteFilteredContactsCommandValidator : AbstractValidator<DeleteFilteredContactsCommand>
{
    public DeleteFilteredContactsCommandValidator()
    {
        RuleFor(account => account.Keyword)
            .MaximumLength(255);
    }
}