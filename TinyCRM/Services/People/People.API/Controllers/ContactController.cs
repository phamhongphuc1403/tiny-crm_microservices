using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Commands.ContactCommands.Requests;
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
    [Authorize(Policy = TinyCrmPermissions.Contacts.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<ContactSummaryDto>>> GetFilteredAndPagedAsync(
        [FromQuery] FilterAndPagingContactsDto dto)
    {
        var contacts = await _mediator.Send(new FilterAndPagingContactQuery(dto));

        return Ok(contacts);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    [Authorize(Policy = TinyCrmPermissions.Contacts.Read)]
    public async Task<ActionResult<ContactDetailDto>> GetByIdAsync(Guid id)
    {
        var contact = await _mediator.Send(new GetContactByIdQuery(id));

        return Ok(contact);
    }

    [HttpPost]
    [Authorize(Policy = TinyCrmPermissions.Contacts.Create)]
    public async Task<ActionResult<ContactDetailDto>> CreateAsync(CreateOrEditContactDto dto)
    {
        var contact = await _mediator.Send(new CreateContactCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = contact.Id }, contact);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Contacts.Edit)]
    public async Task<ActionResult<ContactDetailDto>> UpdateAsync(Guid id, CreateOrEditContactDto dto)
    {
        var contact = await _mediator.Send(new EditContactCommand(id, dto));

        return Ok(contact);
    }

    [HttpDelete]
    [Authorize(Policy = TinyCrmPermissions.Contacts.Delete)]
    public async Task<ActionResult> DeleteAsync(DeleteManyContactsDto dto)
    {
        await _mediator.Send(new DeleteManyContactsCommand(dto));

        return NoContent();
    }

    [HttpDelete("all")]
    [Authorize(Policy = TinyCrmPermissions.Contacts.Delete)]
    public async Task<ActionResult> DeleteAllAsync([FromQuery] FilterContactsDto dto)
    {
        await _mediator.Send(new DeleteFilteredContactsCommand(dto));

        return NoContent();
    }

    [HttpGet("account/{accountId:guid}/contacts")]
    [Authorize(Policy = TinyCrmPermissions.Contacts.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<ContactSummaryDto>>> GetContacts(Guid accountId,
        [FromQuery] FilterAndPagingContactsDto dto)
    {
        var contacts = await _mediator.Send(new FilterAndPagingContactsByAccountIdQuery(accountId, dto));

        return contacts;
    }
}