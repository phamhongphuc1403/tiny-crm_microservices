using BuildingBlock.Application.IntegrationEvents.Handlers;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Exceptions;
using People.Domain.AccountAggregate.Services;
using People.Domain.AccountAggregate.Specifications;

namespace People.Application.IntegrationEvents.Handlers;

public class DeletedWonDealIntegrationEventHandler : IIntegrationEventHandler<DeletedWonDealIntegrationEvent>
{
    private readonly IAccountService _accountService;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletedWonDealIntegrationEventHandler(IAccountService accountService,
        IOperationRepository<Account> operationRepository, IUnitOfWork unitOfWork,
        IReadOnlyRepository<Account> accountReadOnlyRepository)
    {
        _accountService = accountService;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public async Task Handle(DeletedWonDealIntegrationEvent @event)
    {
        var accountIdSpecification = new AccountIdSpecification(@event.AccountId);
        var account = Optional<Account>.Of(await _accountReadOnlyRepository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(@event.AccountId)).Get();

        account = _accountService.UpdateTotalSales(account, @event.ActualRevenue * -1);

        _operationRepository.Update(account);
        await _unitOfWork.SaveChangesAsync();
    }
}