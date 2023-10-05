using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.ProductDTOs;

namespace Sales.Application.CQRS.Commands.ProductCommands.Requests;

public class DeleteFilteredProductsCommand : FilterProductsDto, ICommand
{
    public DeleteFilteredProductsCommand(FilterProductsDto dto)
    {
        Keyword = dto.Keyword;
        Type = dto.Type;
    }
}