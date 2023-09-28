using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.ContactCommands.Requests;
using People.Application.DTOs.ContactDTOs;
using People.Domain.AccountAggregate.Entities;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Services;

namespace People.Application.CQRS.Commands.ContactCommands.Handlers;

public class EditContactCommandHandler : ICommandHandler<EditContactCommand, ContactDetailDto>
{
    private readonly IOperationRepository<Contact> _contactOperationRepository;
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContactService _contactService;

    public EditContactCommandHandler(IOperationRepository<Contact> contactOperationRepository,
        IReadOnlyRepository<Account> accountReadOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork,
        IContactService contactService)
    {
        _contactOperationRepository = contactOperationRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _contactService = contactService;
    }

    public async Task<ContactDetailDto> Handle(EditContactCommand request, CancellationToken cancellationToken)
    {
        var contact =
            await _contactService.EditAsync(request.Id, request.Name, request.Email, request.Phone, request.AccountId);

        _contactOperationRepository.Update(contact);

        await _unitOfWork.SaveChangesAsync();

        await _contactService.AddAccount(contact, request.AccountId);

        return _mapper.Map<ContactDetailDto>(contact);
    }
}