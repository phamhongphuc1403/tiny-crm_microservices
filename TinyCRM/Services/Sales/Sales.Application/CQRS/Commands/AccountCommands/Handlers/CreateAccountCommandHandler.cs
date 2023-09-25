using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Commands.AccountCommands.Requests;
using Sales.Application.DTOs.Accounts;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.DomainService;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.AccountCommands.Handlers;

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountResultDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IAccountDomainService _accountDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public CreateAccountCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,
        IAccountDomainService accountDomainService, IReadOnlyRepository<Account> readOnlyRepository,
        IOperationRepository<Account> operationRepository, IEventBus eventBus)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _accountDomainService = accountDomainService;
        _readOnlyRepository = readOnlyRepository;
        _operationRepository = operationRepository;
        _eventBus = eventBus;
    }

    public async Task<AccountResultDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountDomainService.CreateAsync(request.Name!, request.Email);
        await _operationRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();

        var accountIdSpecification = new AccountIdSpecification(account.Id);
        
        var accountResult = Optional<Account>.Of(await _readOnlyRepository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(account.Id)).Get();
        
        var createAccountEvent = new AccountCreatedIntegrationEvent(accountResult.Id, accountResult.Name, accountResult.Email);
        
        _eventBus.Publish(createAccountEvent);

        return _mapper.Map<AccountResultDto>(accountResult);
    }
}