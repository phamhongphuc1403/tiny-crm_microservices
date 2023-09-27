using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;

namespace People.API.Controllers.AccountControllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountV1Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountV1Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<FilterAndPagingResultDto<AccountSummaryDto>>> GetFilteredAndPagedAsync(
        [FromQuery] FilterAndPagingAccountsDto dto)
    {
        var accounts = await _mediator.Send(new FilterAndPagingAccountsQuery(dto));

        return Ok(accounts);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<ActionResult<AccountDetailDto>> GetByIdAsync(Guid id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<AccountDetailDto>> CreateAsync(CreateOrEditAccountDto dto)
    {
        var account = await _mediator.Send(new CreateAccountCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = account.Id }, account);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AccountDetailDto>> UpdateAsync(Guid id, CreateOrEditAccountDto dto)
    {
        var account = await _mediator.Send(new EditAccountCommand(id, dto));

        return Ok(account);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(DeleteManyAccountsDto dto)
    {
        await _mediator.Send(new DeleteManyAccountsCommand(dto));

        return NoContent();
    }
}