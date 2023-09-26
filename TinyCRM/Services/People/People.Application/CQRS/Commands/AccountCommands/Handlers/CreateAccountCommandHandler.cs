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

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountDetailDto>
{
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IOperationRepository<Account> operationRepository,
        IReadOnlyRepository<Account> readonlyRepository, IUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus)
    {
        _operationRepository = operationRepository;
        _readonlyRepository = readonlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<AccountDetailDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await Account.CreateAsync(request.Name!, request.Email, request.Phone, request.Address,
            _readonlyRepository);

        await _operationRepository.AddAsync(account);

        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new AccountCreatedOrUpdatedIntegrationEvent(account.Id, account.Name, account.Email));

        return _mapper.Map<AccountDetailDto>(account);
    }
}