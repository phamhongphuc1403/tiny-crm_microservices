using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.DTOs.AccountDTOs;
using People.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Services;

namespace People.Application.CQRS.Commands.AccountCommands.Handlers;

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountDetailDto>
{
    private readonly IAccountService _accountService;
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IOperationRepository<Account> operationRepository, IUnitOfWork unitOfWork,
        IMapper mapper, IEventBus eventBus, IAccountService accountService)
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventBus = eventBus;
        _accountService = accountService;
    }

    public async Task<AccountDetailDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountService.CreateAsync(request.Name, request.Email, request.Phone, request.Address);

        await _operationRepository.AddAsync(account);

        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new AccountPeopleCreatedIntegrationEvent(account.Id, account.Name, account.Email));

        return _mapper.Map<AccountDetailDto>(account);
    }
}