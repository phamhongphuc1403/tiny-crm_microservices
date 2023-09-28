using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.ContactCommands.Requests;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Services;

namespace People.Application.CQRS.Commands.ContactCommands.Handlers;

public class DeleteManyContactsCommandHandler : ICommandHandler<DeleteManyContactsCommand>
{
    private readonly IContactService _contactService;
    private readonly IEventBus _eventBus;
    private readonly IOperationRepository<Contact> _operationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyContactsCommandHandler(IUnitOfWork unitOfWork, IOperationRepository<Contact> operationRepository,
        IEventBus eventBus, IContactService contactService)
    {
        _unitOfWork = unitOfWork;
        _operationRepository = operationRepository;
        _eventBus = eventBus;
        _contactService = contactService;
    }

    public async Task Handle(DeleteManyContactsCommand request, CancellationToken cancellationToken)
    {
        var contacts = await _contactService.DeleteManyAsync(request.Ids);

        _operationRepository.RemoveRange(contacts);

        await _unitOfWork.SaveChangesAsync();
    }
}