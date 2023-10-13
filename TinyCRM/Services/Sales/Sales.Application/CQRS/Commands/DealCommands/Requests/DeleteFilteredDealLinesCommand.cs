using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class DeleteFilteredDealLinesCommand : FilterDealLinesDto, ICommand<DealActualRevenueDto>
{
    public DeleteFilteredDealLinesCommand(Guid dealId, FilterDealLinesDto dto)
    {
        Keyword = dto.Keyword;
        DealId = dealId;
    }

    public Guid DealId { get; }
}