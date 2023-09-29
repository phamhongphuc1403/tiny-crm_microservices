using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;

namespace People.API.Controllers.AccountControllers;

[ApiController]
[Route("api/v2/accounts")]
public class AccountV2Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountV2Controller(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Read)]
    public async Task<ActionResult<IEnumerable<AccountSummaryDto>>> GetFilteredAsync([FromQuery] FilterAccountsDto dto)
    {
        var accounts = await _mediator.Send(new FilterAccountsQuery(dto));

        return Ok(accounts);
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteAllAsync([FromQuery] FilterAccountsDto dto)
    {
        await _mediator.Send(new DeleteFilteredAccountsCommand(dto));

        return NoContent();
    }
}