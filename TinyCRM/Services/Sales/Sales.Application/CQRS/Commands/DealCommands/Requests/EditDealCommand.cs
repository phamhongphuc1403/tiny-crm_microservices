using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class EditDealCommand: DealEditDto, ICommand<DealDetailDto>
{
    public Guid Id { get; set; }

    public EditDealCommand(Guid id,DealEditDto dto)
    {
        Id = id;
        Title = dto.Title;
        CustomerId = dto.CustomerId;
        LeadId = dto.LeadId;
        Description = dto.Description;
        EstimatedRevenue = dto.EstimatedRevenue;
        ActualRevenue = dto.ActualRevenue;
    }
}