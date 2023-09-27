using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;

namespace People.API.Controllers;

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
    public async Task<ActionResult<IEnumerable<AccountSummaryDto>>> GetAll()
    {
        var accounts = await _mediator.Send(new GetAllAccountsQuery());

        return Ok(accounts);
    }
}