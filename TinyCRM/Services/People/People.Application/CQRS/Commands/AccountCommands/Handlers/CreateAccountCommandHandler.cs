using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.Requests;
using People.Application.DTOs;
using People.Domain.Entities;

namespace People.Application.CQRS.Commands.Handlers;

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler
    (
        IOperationRepository<Account> operationRepository,
        IReadOnlyRepository<Account> readonlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _operationRepository = operationRepository;
        _readonlyRepository = readonlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AccountDetailDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await Account.CreateAsync(request.Name!, request.Email, request.Phone, request.Address,
            _readonlyRepository);

        await _operationRepository.AddAsync(account);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AccountDetailDto>(account);
    }
}