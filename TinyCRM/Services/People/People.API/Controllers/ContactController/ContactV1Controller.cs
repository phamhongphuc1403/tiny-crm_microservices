using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Commands.ContactCommands.Requests;
using People.Application.CQRS.Queries.ContactQueries.Requests;
using People.Application.DTOs.ContactDTOs;

namespace People.API.Controllers.ContactController;

[ApiController]
[Route("api/v1/contacts")]
public class ContactV1Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactV1Controller(IMediator mediator)
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

    [HttpPost]
    public async Task<ActionResult<ContactDetailDto>> CreateAsync(CreateOrEditContactDto dto)
    {
        var contact = await _mediator.Send(new CreateContactCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = contact.Id }, contact);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ContactDetailDto>> UpdateAsync(Guid id, CreateOrEditContactDto dto)
    {
        var contact = await _mediator.Send(new EditContactCommand(id, dto));

        return Ok(contact);
    }
}