using BuildingBlock.Application.IntegrationEvents.Handlers;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Services;

namespace People.Application.IntegrationEvents.Handlers;

public class AccountSaleCreatedIntegrationEventHandler : IIntegrationEventHandler<AccountSaleCreatedIntegrationEvent>
{
    private readonly IAccountService _accountService;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountSaleCreatedIntegrationEventHandler(IOperationRepository<Account> operationRepository,
        IUnitOfWork unitOfWork, IAccountService accountService)
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _accountService = accountService;
    }

    public async Task Handle(AccountSaleCreatedIntegrationEvent @event)
    {
        var account = await _accountService.CreateAsync(@event.AccountId, @event.Name, @event.Email);

        await _operationRepository.AddAsync(account);

        await _unitOfWork.SaveChangesAsync();
    }
}