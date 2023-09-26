using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.DTOs.AccountDTOs;
using People.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;

namespace People.Application.CQRS.Commands.AccountCommands.Handlers;

public class EditAccountCommandHandler : ICommandHandler<EditAccountCommand, AccountDetailDto>
{
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        IOperationRepository<Account> operationRepository, IReadOnlyRepository<Account> readonlyRepository,
        IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _readonlyRepository = readonlyRepository;
        _eventBus = eventBus;
    }

    public async Task<AccountDetailDto> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await Account.EditAsync(request.Id, request.Name!, request.Email, request.Phone, request.Address,
            _readonlyRepository);

        _operationRepository.Update(account);

        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new AccountCreatedOrUpdatedIntegrationEvent(account.Id, account.Name, account.Email));

        return _mapper.Map<AccountDetailDto>(account);
    }
}