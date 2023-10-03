using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.ContactCommands.Requests;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Specifications;

namespace People.Application.CQRS.Commands.ContactCommands.Handlers;

public class DeleteFilteredContactsCommandHandler : ICommandHandler<DeleteFilteredContactsCommand>
{
    private readonly IOperationRepository<Contact> _operationRepository;
    private readonly IReadOnlyRepository<Contact> _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteFilteredContactsCommandHandler(IOperationRepository<Contact> operationRepository,
        IUnitOfWork unitOfWork, IReadOnlyRepository<Contact> readOnlyRepository)
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task Handle(DeleteFilteredContactsCommand request, CancellationToken cancellationToken)
    {
        var contactNamePartialMatchSpecification = new ContactNamePartialMatchSpecification(request.Keyword);

        var contactEmailPartialMatchSpecification = new ContactEmailPartialMatchSpecification(request.Keyword);

        var specification = contactNamePartialMatchSpecification.Or(contactEmailPartialMatchSpecification);

        var contacts = await _readOnlyRepository.GetAllAsync(specification);

        _operationRepository.RemoveRange(contacts);

        await _unitOfWork.SaveChangesAsync();
    }
}