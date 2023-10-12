using FluentValidation;
using Sales.Application.CQRS.Commands.DealCommands.Requests;

namespace Sales.Application.CQRS.Commands.DealCommands.Validators;

public class EditDealLineCommandValidator : AbstractValidator<EditDealLineCommand>
{
    public EditDealLineCommandValidator()
    {
        RuleFor(dl => dl.Quantity)
            .InclusiveBetween(0, int.MaxValue);

        RuleFor(dl => dl.PricePerUnit)
            .InclusiveBetween(0, double.MaxValue);
    }
}