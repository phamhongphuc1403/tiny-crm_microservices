using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;

namespace People.API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<AccountSummaryDto>>> GetFilteredAndPagedAsync(
        [FromQuery] FilterAndPagingAccountsDto dto)
    {
        var accounts = await _mediator.Send(new FilterAndPagingAccountsQuery(dto));

        return Ok(accounts);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Read)]
    public async Task<ActionResult<AccountDetailDto>> GetByIdAsync(Guid id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        return Ok(account);
    }

    [HttpPost]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Create)]
    public async Task<ActionResult<AccountDetailDto>> CreateAsync(CreateOrEditAccountDto dto)
    {
        var account = await _mediator.Send(new CreateAccountCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = account.Id }, account);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Edit)]
    public async Task<ActionResult<AccountDetailDto>> UpdateAsync(Guid id, CreateOrEditAccountDto dto)
    {
        var account = await _mediator.Send(new EditAccountCommand(id, dto));

        return Ok(account);
    }

    [HttpDelete]
    [Authorize(Policy = TinyCrmPermissions.Accounts.Delete)]
    public async Task<ActionResult> DeleteAsync(DeleteManyAccountsDto dto)
    {
        await _mediator.Send(new DeleteManyAccountsCommand(dto));

        return NoContent();
    }

    [HttpDelete("all")]
    public async Task<ActionResult> DeleteAllAsync([FromQuery] FilterAccountsDto dto)
    {
        await _mediator.Send(new DeleteFilteredAccountsCommand(dto));

        return NoContent();
    }
}