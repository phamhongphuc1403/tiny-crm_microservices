using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using Sales.Application.CQRS.Commands.ProductCommands.Requests;
using Sales.Domain.ProductAggregate.DomainService;
using Sales.Domain.ProductAggregate.Repositories;

namespace Sales.Application.CQRS.Commands.ProductCommands.Handlers;

public class DeleteManyProductsCommandHandler : ICommandHandler<DeleteManyProductsCommand>
{
    private readonly IProductOperationRepository _operationRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyProductsCommandHandler(IProductOperationRepository operationRepository,
        IProductDomainService productDomainService, IUnitOfWork unitOfWork)
    {
        _operationRepository = operationRepository;
        _productDomainService = productDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteManyProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await _productDomainService.RemoveManyAsync(request.Ids);

        _operationRepository.SoftRemoveRange(products);

        await _unitOfWork.SaveChangesAsync();
    }
}