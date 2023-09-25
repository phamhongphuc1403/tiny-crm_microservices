using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.CQRS.Commands.AccountCommands.Requests;
using Sales.Application.CQRS.Queries.AccountQueries.Requests;
using Sales.Application.DTOs.Accounts;

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
    public async Task<ActionResult<FilterResultDto<AccountResultDto>>> GetAllAsync(
        [FromQuery] FilterAccountDto dto)
    {
        var accounts = await _mediator.Send(new FilterAccountsQuery(dto));

        return Ok(accounts);
    }
    
    [HttpPost]
    public async Task<ActionResult<AccountResultDto>> CreateAsync(CreateAccountDto dto)
    {
        var account = await _mediator.Send(new CreateAccountCommand(dto));

        return Ok(account);
    }
}