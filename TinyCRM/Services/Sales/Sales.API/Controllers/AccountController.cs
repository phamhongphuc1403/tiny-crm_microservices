using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.CQRS.Commands.AccountCommands.Requests;
using Sales.Application.CQRS.Queries.AccountQueries.Requests;
using Sales.Application.DTOs.AccountDTOs;

namespace Sales.API.Controllers;

[ApiController]
[Route("api/crm-accounts")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Read)]
    public async Task<ActionResult<IEnumerable<AccountSummaryDto>>> FilteredAsync(
        [FromQuery] FilterAndPagingAccountsDto dto)
    {
        var accounts = await _mediator.Send(new FilterAndPagingAccountsQuery(dto));

        return Ok(accounts);
    }

    [HttpPost]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Create)]
    public async Task<ActionResult<AccountSummaryDto>> CreateAsync(CreateAccountDto dto)
    {
        var account = await _mediator.Send(new CreateAccountCommand(dto));

        return Ok(account);
    }
}