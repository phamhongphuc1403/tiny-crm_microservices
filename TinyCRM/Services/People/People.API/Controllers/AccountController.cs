using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Queries.Requests;
using People.Application.DTOs;

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
    public async Task<ActionResult<FilterAndPagingResultDto<AccountSummaryDto>>> GetFilteredAndPagedAsync(
        [FromQuery] FilterAndPagingAccountsDto dto)
    {
        var accounts = await _mediator.Send(new FilterAndPagingAccountsQuery(dto));

        return Ok(accounts);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AccountDetailDto>> GetByIdAsync(Guid id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        return Ok(account);
    }
}