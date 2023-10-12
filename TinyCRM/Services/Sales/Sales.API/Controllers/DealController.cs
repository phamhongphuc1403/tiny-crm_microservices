using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.API.Controllers;

[ApiController]
[Route("api/deals")]
public class DealController : ControllerBase
{
    private readonly IMediator _mediator;

    public DealController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Deals.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<DealSummaryDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingDealsDto dto)
    {
        var deals = await _mediator.Send(new FilterAndPagingDealsQuery(dto));

        return Ok(deals);
    }

    [HttpPost]
    [Authorize(Policy = TinyCrmPermissions.Deals.Create)]
    public async Task<ActionResult<DealDetailDto>> CreateAsync([FromBody] DealCreateDto dto)
    {
        var deal = await _mediator.Send(new CreateDealCommand(dto));

        return Ok(deal);
    }

    [HttpPut("{dealId:guid}/lines")]
    public async Task<ActionResult<DealLineDto>> AddProductAsync(Guid dealId, [FromBody] CreateOrEditDealLineDto dto)
    {
        var dealLine = await _mediator.Send(new CreateDealLineCommand(dealId, dto));

        return Ok(dealLine);
    }

    [HttpGet("{dealId:guid}/lines/{dealLineId:guid}")]
    public async Task<ActionResult<DealLineDto>> GetDealLinesAsync(Guid dealId, Guid dealLineId)
    {
        var dealLine = await _mediator.Send(new GetDeaLineByIdQuery(dealId, dealLineId));

        return Ok(dealLine);
    }

    [HttpGet("lead/{leadId:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Deals.Read)]
    public async Task<ActionResult<DealDetailDto>> GetByLeadIdAsync(Guid leadId)
    {
        var deal = await _mediator.Send(new GetDealByLeadIdQuery(leadId));

        return Ok(deal);
    }

    [HttpGet("{dealId:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Deals.Read)]
    public async Task<ActionResult<DealDetailDto>> GetByDealIdAsync(Guid dealId)
    {
        var deal = await _mediator.Send(new GetDealByDealIdQuery(dealId));

        return Ok(deal);
    }
    
    [HttpPut("{dealId:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Deals.Edit)]
    public async Task<ActionResult<DealDetailDto>> EditAsync(Guid dealId, [FromBody] DealEditDto dto)
    {
        var deal = await _mediator.Send(new EditDealCommand(dealId, dto));

        return Ok(deal);
    }
    
    [HttpPut("{dealId:guid}/close-as-won")]
    [Authorize(Policy = TinyCrmPermissions.Deals.Edit)]
    public async Task<ActionResult<DealDetailDto>> CloseAsWonAsync(Guid dealId)
    {
        var deal = await _mediator.Send(new CloseAsWonDealCommand(dealId));

        return Ok(deal);
    }
    
    [HttpPut("{dealId:guid}/close-as-lost")]
    [Authorize(Policy = TinyCrmPermissions.Deals.Edit)]
    public async Task<ActionResult<DealDetailDto>> CloseAsLostAsync(Guid dealId)
    {
        var deal = await _mediator.Send(new CloseAsLostDealCommand(dealId));

        return Ok(deal);
    }
}