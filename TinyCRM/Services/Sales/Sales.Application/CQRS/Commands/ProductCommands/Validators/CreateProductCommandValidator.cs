using FluentValidation;
using Sales.Application.CQRS.Commands.ProductCommands.Requests;

namespace Sales.Application.CQRS.Commands.ProductCommands.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Code)
            .NotEmpty()
            .When(product => string.IsNullOrWhiteSpace(product.Code))
            .MaximumLength(10);

        RuleFor(product => product.Name)
            .NotEmpty()
            .When(product => string.IsNullOrWhiteSpace(product.Name))
            .MaximumLength(255);

        RuleFor(product => product.Price)
            .InclusiveBetween(0, double.MaxValue);
    }
}