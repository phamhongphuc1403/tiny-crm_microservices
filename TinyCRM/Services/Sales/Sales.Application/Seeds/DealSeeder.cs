using Bogus;
using BuildingBlock.Application;
using BuildingBlock.Application.Constants;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Sales.Domain.AccountAggregate;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Application.Seeds;

public class DealSeeder : IDataSeeder
{
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IReadOnlyRepository<Deal> _dealReadOnlyRepository;
    private readonly ILogger<DealSeeder> _logger;
    private readonly IReadOnlyRepository<Product> _productReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DealSeeder(IReadOnlyRepository<Deal> dealReadOnlyRepository, ILogger<DealSeeder> logger,
        IUnitOfWork unitOfWork, IOperationRepository<Deal> dealOperationRepository,
        IReadOnlyRepository<Account> accountReadOnlyRepository, IReadOnlyRepository<Product> productReadOnlyRepository,
        IDealDomainService dealDomainService)
    {
        _dealReadOnlyRepository = dealReadOnlyRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _dealOperationRepository = dealOperationRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
        _productReadOnlyRepository = productReadOnlyRepository;
        _dealDomainService = dealDomainService;
    }

    public async Task SeedDataAsync()
    {
        if (await _dealReadOnlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Deal data already seeded!");
            return;
        }

        var accountIds = (await _accountReadOnlyRepository.GetAllAsync()).Select(account => account.Id).ToList();

        var productIds = (await _productReadOnlyRepository.GetAllAsync()).Select(product => product.Id).ToList();

        var deals = SeedDeals(accountIds).ToList();

        await _dealOperationRepository.AddRangeAsync(deals);

        await _unitOfWork.SaveChangesAsync();

        foreach (var deal in deals)
        {
            if (deal.DealStatus is not DealStatus.Open) continue;

            var dealLines = SeedDealLines(deal, productIds);

            foreach (var dealLine in dealLines)
                await _dealDomainService.CreateDealLineAsync(deal, dealLine.ProductId, dealLine.PricePerUnit,
                    dealLine.Quantity);

            _dealOperationRepository.Update(deal);
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Deal data seeded successfully!");
    }

    private IEnumerable<Deal> SeedDeals(IEnumerable<Guid> accountIds)
    {
        var faker = new Faker<Deal>()
            .RuleFor(deal => deal.Id, f => f.Random.Guid())
            .RuleFor(deal => deal.Description, f => f.Lorem.Paragraph())
            .RuleFor(deal => deal.EstimatedRevenue, f => f.Random.Double(0, 1000000))
            .RuleFor(deal => deal.CreatedDate, f => f.Date.Past())
            .RuleFor(deal => deal.Title, f => f.Lorem.Sentence())
            .RuleFor(deal => deal.DealStatus, f => f.PickRandom<DealStatus>())
            .RuleFor(deal => deal.CustomerId, f => f.PickRandom(accountIds))
            .RuleFor(deal => deal.DealLines, _ => new List<DealLine>());

        return faker.Generate(SeedConstant.NumberOfRecords);
    }

    private IEnumerable<DealLine> SeedDealLines(Deal deal, IEnumerable<Guid> productIds)
    {
        var faker = new Faker<DealLine>()
            .RuleFor(dl => dl.DealId, _ => deal.Id)
            .RuleFor(dl => dl.Quantity, f => f.Random.Int(1, 1000))
            .RuleFor(dl => dl.ProductId, f => f.PickRandom(productIds))
            .RuleFor(dl => dl.PricePerUnit, f => f.Random.Double(100, 10000))
            .RuleFor(dl => dl.TotalAmount, (_, dl) => dl.Quantity * dl.PricePerUnit);

        return faker.Generate(SeedConstant.NumberOfRecords);
    }
}