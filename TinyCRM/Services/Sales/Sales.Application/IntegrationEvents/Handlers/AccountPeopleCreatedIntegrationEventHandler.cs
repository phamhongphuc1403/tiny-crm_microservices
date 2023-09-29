using BuildingBlock.Application.IntegrationEvents.Handlers;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.DomainService;

namespace Sales.Application.IntegrationEvents.Handlers;

public class
    AccountPeopleCreatedIntegrationEventHandler : IIntegrationEventHandler<AccountPeopleCreatedIntegrationEvent>
{
    private readonly IAccountDomainService _accountDomainService;
    private readonly ILogger<AccountPeopleCreatedIntegrationEventHandler> _logger;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountPeopleCreatedIntegrationEventHandler(ILogger<AccountPeopleCreatedIntegrationEventHandler> logger,
        IAccountDomainService accountDomainService, IUnitOfWork unitOfWork,
        IOperationRepository<Account> operationRepository)
    {
        _logger = logger;
        _accountDomainService = accountDomainService;
        _unitOfWork = unitOfWork;
        _operationRepository = operationRepository;
    }

    public async Task Handle(AccountPeopleCreatedIntegrationEvent @event)
    {
        var account = await _accountDomainService.CreateAsync(@event.AccountId, @event.Name, @event.Email);
        await _operationRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"Created account {account.Id}");
    }
}