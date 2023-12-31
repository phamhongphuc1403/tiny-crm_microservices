using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.AccountCommands.Requests;
using Sales.Application.DTOs.AccountDTOs;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.DomainService;

namespace Sales.Application.CQRS.Commands.AccountCommands.Handlers;

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountSummaryDto>
{
    private readonly IAccountDomainService _accountDomainService;
    private readonly IEventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

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

    public async Task<AccountSummaryDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountDomainService.CreateAsync(request.Name!, request.Email);
        await _operationRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new AccountSaleCreatedIntegrationEvent(account.Id, account.Email, account.Name));

        return _mapper.Map<AccountSummaryDto>(account);
    }
}