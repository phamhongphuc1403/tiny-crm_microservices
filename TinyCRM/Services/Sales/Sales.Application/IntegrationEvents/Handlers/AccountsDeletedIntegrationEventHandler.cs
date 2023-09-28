using BuildingBlock.Application.IntegrationEvents.Handlers;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Sales.Application.CQRS.Commands.AccountCommands.Requests;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.DomainService;

namespace Sales.Application.IntegrationEvents.Handlers;

public class AccountsDeletedIntegrationEventHandler : IIntegrationEventHandler<AccountsDeletedIntegrationEvent>
{
    private readonly ILogger<AccountsDeletedIntegrationEventHandler> _logger;
    private readonly IAccountDomainService _accountService;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AccountsDeletedIntegrationEventHandler(ILogger<AccountsDeletedIntegrationEventHandler> logger, IAccountDomainService accountService, IOperationRepository<Account> operationRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _accountService = accountService;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AccountsDeletedIntegrationEvent @event)
    {
        var accounts = await _accountService.DeleteManyAsync(@event.AccountIds);

        _operationRepository.RemoveRange(accounts);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"Deleted accounts {@event.AccountIds}");
    }
}