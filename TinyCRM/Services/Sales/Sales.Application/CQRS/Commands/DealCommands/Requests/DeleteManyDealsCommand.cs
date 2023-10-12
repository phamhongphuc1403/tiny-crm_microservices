using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class DeleteManyDealsCommand: DealDeleteManyDto,ICommand
{
    public DeleteManyDealsCommand(DealDeleteManyDto dto)
    {
        Ids = dto.Ids;
    }
}