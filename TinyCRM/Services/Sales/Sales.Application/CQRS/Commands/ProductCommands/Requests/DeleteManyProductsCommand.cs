using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.ProductDTOs;

namespace Sales.Application.CQRS.Commands.ProductCommands.Requests;

public class DeleteManyProductsCommand : DeleteManyProductsDto, ICommand
{
    public DeleteManyProductsCommand(DeleteManyProductsDto dto)
    {
        Ids = dto.Ids;
    }
}