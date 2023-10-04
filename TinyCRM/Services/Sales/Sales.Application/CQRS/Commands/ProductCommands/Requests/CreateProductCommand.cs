using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.ProductDTOs;

namespace Sales.Application.CQRS.Commands.ProductCommands.Requests;

public class CreateProductCommand : CreateOrEditProductDto, ICommand<ProductDetailDto>
{
    public CreateProductCommand(CreateOrEditProductDto dto)
    {
        Code = dto.Code;
        Name = dto.Name;
        Price = dto.Price;
        IsAvailable = dto.IsAvailable;
        Type = dto.Type;
    }
}