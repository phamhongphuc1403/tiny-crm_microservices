using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.ProductCommands.Requests;
using Sales.Application.DTOs.ProductDTOs;
using Sales.Domain.ProductAggregate.DomainService;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Application.CQRS.Commands.ProductCommands.Handlers;

public class EditProductCommandHandler : ICommandHandler<EditProductCommand, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCommandHandler(IProductDomainService productDomainService, IMapper mapper,
        IOperationRepository<Product> operationRepository, IUnitOfWork unitOfWork)
    {
        _productDomainService = productDomainService;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDetailDto> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productDomainService.EditAsync(request.Id, request.Code, request.Name, request.Price,
            request.IsAvailable, request.Type);

        _operationRepository.Update(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDetailDto>(product);
    }
}