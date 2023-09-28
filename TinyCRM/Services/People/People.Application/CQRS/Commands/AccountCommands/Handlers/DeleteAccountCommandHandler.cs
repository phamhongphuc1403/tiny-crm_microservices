using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Services;

namespace People.Application.CQRS.Commands.AccountCommands.Handlers;

public class DeleteAccountCommandHandler : ICommandHandler<DeleteManyAccountsCommand>
{
    private readonly IAccountService _accountService;
    private readonly IEventBus _eventBus;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountCommandHandler(IUnitOfWork unitOfWork, IOperationRepository<Account> operationRepository,
        IEventBus eventBus, IAccountService accountService)
    {
        _unitOfWork = unitOfWork;
        _operationRepository = operationRepository;
        _eventBus = eventBus;
        _accountService = accountService;
    }

    public async Task Handle(DeleteManyAccountsCommand request, CancellationToken cancellationToken)
    {
        var accounts = await _accountService.DeleteManyAsync(request.Ids);

        _operationRepository.RemoveRange(accounts);

        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new AccountsDeletedIntegrationEvent(request.Ids));
    }
}