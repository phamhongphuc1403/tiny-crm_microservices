using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.CQRS.Commands.LeadCommands.Requests;
using Sales.Application.CQRS.Queries.LeadQueries.Requests;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Application.DTOs.LeadDTOs.Enums;

namespace Sales.API.Controllers;

[ApiController]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Leads.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<LeadSummaryDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingLeadsDto dto)
    {
        var leads = await _mediator.Send(new FilterAndPagingLeadsQuery(dto));

        return Ok(leads);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    [Authorize(Policy = TinyCrmPermissions.Leads.Read)]
    public async Task<ActionResult<LeadDetailDto>> GetByIdAsync(Guid id)
    {
        var lead = await _mediator.Send(new GetLeadQuery(id));

        return Ok(lead);
    }

    [HttpPost]
    [Authorize(Policy = TinyCrmPermissions.Leads.Create)]
    public async Task<ActionResult<LeadDetailDto>> CreateAsync(LeadCreateDto dto)
    {
        var lead = await _mediator.Send(new CreateLeadCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = lead.Id }, lead);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Leads.Edit)]
    public async Task<ActionResult<LeadDetailDto>> EditAsync(Guid id, LeadEditDto dto)
    {
        var lead = await _mediator.Send(new EditLeadCommand(id, dto));

        return Ok(lead);
    }

    [HttpPut("{id:guid}/disqualify")]
    [Authorize(Policy = TinyCrmPermissions.Leads.Edit)]
    public async Task<ActionResult<LeadDetailDto>> DisqualifyAsync(Guid id, LeadDisqualifyDto dto)
    {
        var lead = await _mediator.Send(new DisqualifyLeadCommand(id, dto));

        return Ok(lead);
    }

    [HttpDelete]
    [Authorize(Policy = TinyCrmPermissions.Leads.Delete)]
    public async Task<ActionResult> DeleteManyAsync(LeadDeleteManyDto dto)
    {
        await _mediator.Send(new DeleteManyLeadsCommand(dto));

        return NoContent();
    }

    [HttpDelete("all")]
    [Authorize(Policy = TinyCrmPermissions.Leads.Delete)]
    public async Task<ActionResult> DeleteAllAsync([FromQuery] FilterLeadsDto dto)
    {
        await _mediator.Send(new DeleteFilterLeadsCommand(dto));

        return NoContent();
    }

    [HttpPut("{id:guid}/qualify")]
    [Authorize(Policy = TinyCrmPermissions.Leads.Edit)]
    public async Task<ActionResult<LeadDetailDto>> QualifyAsync(Guid id)
    {
        var lead = await _mediator.Send(new QualifyLeadCommand(id));

        return Ok(lead);
    }

    [HttpGet("account/{accountId:guid}/leads-valid")]
    [Authorize(Policy = TinyCrmPermissions.Leads.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<LeadSummaryDto>>> GetByAccountIdAsync(
        [FromQuery] FilterAndPagingDto<LeadSortProperty> dto, Guid accountId)
    {
        var leads = await _mediator.Send(new GetLeadsValidByAccountIdQuery(dto, accountId));

        return Ok(leads);
    }

    [HttpGet("account/{accountId:guid}/leads")]
    [Authorize(Policy = TinyCrmPermissions.Leads.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<LeadSummaryDto>>> GetByAccountIdAsync(
        [FromQuery] FilterAndPagingLeadsDto dto,Guid accountId)
    {
        var leads = await _mediator.Send(new GetLeadsByAccountIdQuery(dto,accountId));

        return Ok(leads);
    }
    
    [HttpGet("statistics")]
    [Authorize(Policy = TinyCrmPermissions.Leads.Read)]
    public async Task<ActionResult<LeadStatisticsDto>> GetStatisticsAsync()
    {
        var statistics = await _mediator.Send(new GetStatisticsLeadQuery());

        return Ok(statistics);
    }
}