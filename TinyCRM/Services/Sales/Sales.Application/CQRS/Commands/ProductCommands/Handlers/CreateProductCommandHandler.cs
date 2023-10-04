using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.ProductCommands.Requests;
using Sales.Application.DTOs.ProductDTOs;
using Sales.Domain.ProductAggregate.DomainService;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Application.CQRS.Commands.ProductCommands.Handlers;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationRepository<Product> _operationRepository;
    private readonly IProductDomainService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductDomainService productService, IMapper mapper,
        IOperationRepository<Product> operationRepository, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _mapper = mapper;
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDetailDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productService.CreateAsync(request.Code, request.Name, request.Price, request.IsAvailable,
            request.Type);

        await _operationRepository.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductDetailDto>(product);
    }
}