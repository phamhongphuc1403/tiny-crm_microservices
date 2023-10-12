using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class DeleteFilterDealsCommand:FilterDealsDto,ICommand
{
    public DeleteFilterDealsCommand(FilterDealsDto dto)
    {
        Status = dto.Status;
        Keyword = dto.Keyword;
    }
}