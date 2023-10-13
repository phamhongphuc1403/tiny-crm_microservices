using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class CreateDealLineCommand : CreateOrEditDealLineDto, ICommand<DealLineWithDealActualRevenueDto>
{
    public CreateDealLineCommand(Guid dealId, CreateOrEditDealLineDto dto)
    {
        DealId = dealId;
        ProductId = dto.ProductId;
        Quantity = dto.Quantity;
        PricePerUnit = dto.PricePerUnit;
    }

    public Guid DealId { get; private set; }
}