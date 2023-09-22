using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Application.DTOs.AccountDTOs;
using People.Domain.AccountAggregate.Entities;

namespace People.Application.CQRS.Commands.AccountCommands.Handlers;

public class EditAccountCommandHandler : ICommandHandler<EditAccountCommand, AccountDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditAccountCommandHandler
    (
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IOperationRepository<Account> operationRepository,
        IReadOnlyRepository<Account> readonlyRepository
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _readonlyRepository = readonlyRepository;
    }

    public async Task<AccountDetailDto> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await Account.EditAsync(request.Id, request.Name!, request.Email, request.Phone, request.Address,
            _readonlyRepository);

        _operationRepository.Update(account);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AccountDetailDto>(account);
    }
}