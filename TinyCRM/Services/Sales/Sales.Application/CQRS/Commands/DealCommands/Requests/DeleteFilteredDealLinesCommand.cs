using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class DeleteFilteredDealLinesCommand : FilterDealLinesDto, ICommand
{
    public DeleteFilteredDealLinesCommand(Guid dealId, FilterDealLinesDto dto)
    {
        Keyword = dto.Keyword;
        DealId = dealId;
    }

    public Guid DealId { get; }
}