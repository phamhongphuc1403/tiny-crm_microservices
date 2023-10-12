using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class CloseAsLostDealCommand:ICommand<DealDetailDto>
{
    public Guid DealId { get; set; }

    public CloseAsLostDealCommand(Guid dealId)
    {
        DealId = dealId;
    }
}