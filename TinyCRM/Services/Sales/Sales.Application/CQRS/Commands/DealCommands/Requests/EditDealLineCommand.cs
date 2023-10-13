using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class EditDealLineCommand : CreateOrEditDealLineDto, ICommand<DealLineWithDealActualRevenueDto>
{
    public EditDealLineCommand(Guid dealId, Guid dealLineId, CreateOrEditDealLineDto dto)
    {
        DealId = dealId;
        DealLineId = dealLineId;
        ProductId = dto.ProductId;
        Quantity = dto.Quantity;
        PricePerUnit = dto.PricePerUnit;
    }

    public Guid DealId { get; set; }
    public Guid DealLineId { get; set; }
}