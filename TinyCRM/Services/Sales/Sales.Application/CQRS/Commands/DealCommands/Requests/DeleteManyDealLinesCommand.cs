using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class DeleteManyDealLinesCommand : DeleteManyDealLinesDto, ICommand
{
    public DeleteManyDealLinesCommand(Guid dealId, DeleteManyDealLinesDto dto)
    {
        DealId = dealId;
        DealLineIds = dto.DealLineIds;
    }

    public Guid DealId { get; }
}