using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Specifications;

namespace People.Application.CQRS.Commands.AccountCommands.Handlers;

public class DeleteFilteredAccountsCommandHandler : ICommandHandler<DeleteFilteredAccountsCommand>
{
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;


    public DeleteFilteredAccountsCommandHandler(IOperationRepository<Account> operationRepository,
        IUnitOfWork unitOfWork, IEventBus eventBus, IReadOnlyRepository<Account> readOnlyRepository)
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task Handle(DeleteFilteredAccountsCommand request, CancellationToken cancellationToken)
    {
        var accountNamePartialMatchSpecification = new AccountNamePartialMatchSpecification(request.Keyword);

        var accountEmailPartialMatchSpecification = new AccountEmailPartialMatchSpecification(request.Keyword);

        var specification = accountNamePartialMatchSpecification.Or(accountEmailPartialMatchSpecification);
        
        var accounts = await _readOnlyRepository.GetAllAsync(specification);
        
        _operationRepository.RemoveRange(accounts);
        
        await _unitOfWork.SaveChangesAsync();
        
        _eventBus.Publish(new AccountsDeletedIntegrationEvent(accounts.Select(x => x.Id)));
    }
}