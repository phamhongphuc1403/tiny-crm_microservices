using BuildingBlock.Application.IntegrationEvents.Handlers;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Microsoft.Extensions.Logging;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.DomainService;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;

namespace Sales.Application.IntegrationEvents.Handlers;

public class AccountEditedIntegrationEventHandler : IIntegrationEventHandler<AccountEditedIntegrationEvent>
{
    private readonly IAccountDomainService _accountDomainService;
    private readonly ILogger<AccountEditedIntegrationEventHandler> _logger;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountEditedIntegrationEventHandler(ILogger<AccountEditedIntegrationEventHandler> logger,
        IAccountDomainService accountDomainService, IReadOnlyRepository<Account> readOnlyRepository,
        IOperationRepository<Account> operationRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _accountDomainService = accountDomainService;
        _readOnlyRepository = readOnlyRepository;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AccountEditedIntegrationEvent @event)
    {
        var account = await CheckValidOnEditExistAsync(@event.AccountId);
        var result = await _accountDomainService.UpdateAsync(account, @event.Name, @event.Email);
        _operationRepository.Update(account);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"Updated account {result.Id}");
    }

    private async Task<Account> CheckValidOnEditExistAsync(Guid id)
    {
        _logger.LogError(id.ToString());
        var accountIdSpecification = new AccountIdSpecification(id);

        var account = Optional<Account>.Of(await _readOnlyRepository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(id)).Get();

        return account;
    }
}