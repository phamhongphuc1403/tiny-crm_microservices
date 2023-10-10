using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var leads = await _mediator.Send(new FilterAndPagingDealsQuery(dto));

        return Ok(leads);
    }
}