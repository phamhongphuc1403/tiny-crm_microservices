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
        var contacts = await _mediator.Send(new FilterAndPagingContactQuery(dto));

        return Ok(contacts);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<ActionResult<ContactDetailDto>> GetByIdAsync(Guid id)
    {
        var contact = await _mediator.Send(new GetContactByIdQuery(id));

        return Ok(contact);
    }
}