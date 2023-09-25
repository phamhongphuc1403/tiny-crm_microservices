using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Queries.ContactQueries.Requests;
using People.Application.DTOs.ContactDTOs;

namespace People.API.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<FilterAndPagingResultDto<ContactSummaryDto>>> GetFilteredAndPagedAsync(
        [FromQuery] FilterAndPagingContactsDto dto)
    {
        var accounts = await _mediator.Send(new FilterAndPagingContactQuery(dto));

        return Ok(accounts);
    }
}