using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Domain.AccountAggregate.Entities;

namespace People.Application.CQRS.Commands.AccountCommands.Handlers;

public class DeleteAccountCommandHandler : ICommandHandler<DeleteManyAccountsCommand>
{
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountCommandHandler
    (
        IUnitOfWork unitOfWork,
        IOperationRepository<Account> operationRepository,
        IReadOnlyRepository<Account> readonlyRepository
    )
    {
        _unitOfWork = unitOfWork;
        _operationRepository = operationRepository;
        _readonlyRepository = readonlyRepository;
    }

    public async Task Handle(DeleteManyAccountsCommand request, CancellationToken cancellationToken)
    {
        var accounts = await Account.DeleteManyAsync(request.Ids, _readonlyRepository);

        _operationRepository.RemoveRange(accounts);

        await _unitOfWork.SaveChangesAsync();
    }
}