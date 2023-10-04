using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.ProductDTOs;

namespace Sales.Application.CQRS.Commands.ProductCommands.Requests;

public class EditProductCommand : CreateOrEditProductDto, ICommand<ProductDetailDto>
{
    public EditProductCommand(Guid id, CreateOrEditProductDto dto)
    {
        Id = id;
        Code = dto.Code;
        Name = dto.Name;
        Price = dto.Price;
        IsAvailable = dto.IsAvailable;
        Type = dto.Type;
    }

    public Guid Id { get; private init; }
}