using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.ProductCommands.Requests;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Repositories;
using Sales.Domain.ProductAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.ProductCommands.Handlers;

public class DeleteFilteredProductsCommandHandler : ICommandHandler<DeleteFilteredProductsCommand>
{
    private readonly IProductOperationRepository _operationRepository;
    private readonly IReadOnlyRepository<Product> _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFilteredProductsCommandHandler(IProductOperationRepository operationRepository, IUnitOfWork unitOfWork,
        IReadOnlyRepository<Product> readOnlyRepository)
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task Handle(DeleteFilteredProductsCommand request, CancellationToken cancellationToken)
    {
        var productCodePartialMatchSpecification = new ProductCodePartialMatchSpecification(request.Keyword);

        var productNamePartialMatchSpecification = new ProductNamePartialMatchSpecification(request.Keyword);

        var productTypeSpecification = new ProductTypeSpecification(request.Type);

        var productDeletedSpecification = new ProductDeletedSpecification(false);

        var productKeywordPartialMatchSpecification =
            productNamePartialMatchSpecification.Or(productCodePartialMatchSpecification);

        var specification = productKeywordPartialMatchSpecification.And(productTypeSpecification)
            .And(productDeletedSpecification);

        var products = await _readOnlyRepository.GetAllAsync(specification);

        _operationRepository.SoftRemoveRange(products);

        await _unitOfWork.SaveChangesAsync();
    }
}