using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Queries.ContactQueries.Requests;
using People.Application.DTOs.ContactDTOs;

namespace People.API.Controllers.ContactController;

[ApiController]
[Route("api/v2/contacts")]
public class ContactV2Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactV2Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Contacts.Read)]
    public async Task<ActionResult<IEnumerable<ContactSummaryDto>>> GetFilteredAsync([FromQuery] FilterContactsDto dto)
    {
        var contacts = await _mediator.Send(new FilterContactsQuery(dto));

        return Ok(contacts);
    }
}