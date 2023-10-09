using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.DealAggregate.DomainService;

public class DealDomainService:IDealDomainService
{
    private readonly IReadOnlyRepository<Deal> _dealReadOnlyRepository;
    private readonly IReadOnlyRepository<Product> _productReadOnlyRepository;
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    public DealDomainService(IReadOnlyRepository<Deal> dealReadOnlyRepository, IReadOnlyRepository<Product> productReadOnlyRepository, IReadOnlyRepository<Account> accountReadOnlyRepository)
    {
        _dealReadOnlyRepository = dealReadOnlyRepository;
        _productReadOnlyRepository = productReadOnlyRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public async Task<Deal> CreateDealAsync(string title, Guid customerId, Guid? leadId, string? description, DealStatus dealStatus,
        decimal estimatedRevenue)
    {
        Optional<bool>
            .Of(await _accountReadOnlyRepository.CheckIfExistAsync(new AccountIdSpecification(customerId)))
            .ThrowIfNotPresent(new AccountNotFoundException(customerId));

        if (leadId != null)
        {
            //Check lead exist && lead of Customer
        }
        
        throw new NotImplementedException();
    }

    public Task<Deal> UpdateDealAsync(Deal deal, string title, Guid customerId, Guid? leadId, string? description, DealStatus dealStatus,
        decimal estimatedRevenue)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> DeleteManyDealAsync(List<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> CreateDealLineAsync(Deal deal, Guid productId, decimal price, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> UpdateDealLineAsync(Deal deal, Guid idDealLine, Guid productId, decimal price, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> DeleteManyDealLinesAsync(Deal deal, List<Guid> idDealLines)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> UpdateDealStatusAsync(Deal deal, DealStatus dealStatus)
    {
        throw new NotImplementedException();
    }
}