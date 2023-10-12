using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class CloseAsWonDealCommand:ICommand<DealDetailDto>
{
    public Guid DealId { get; set; }

    public CloseAsWonDealCommand(Guid dealId)
    {
        DealId = dealId;
    }
}