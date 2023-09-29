using Bogus;
using BuildingBlock.Application;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Fare;
using Microsoft.Extensions.Logging;
using People.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;
using People.Domain.Constants;

namespace People.Application.Seeders;

public class AccountSeeder : IDataSeeder
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<AccountSeeder> _logger;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountSeeder(
        IOperationRepository<Account> operationRepository,
        IReadOnlyRepository<Account> readonlyRepository,
        IUnitOfWork unitOfWork,
        ILogger<AccountSeeder> logger,
        IEventBus eventBus
    )
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _eventBus = eventBus;
        _readonlyRepository = readonlyRepository;
    }

    public async Task SeedDataAsync()
    {
        if (await _readonlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Account data already seeded!");
            return;
        }

        var accounts = GenerateAccounts();

        await _operationRepository.AddRangeAsync(accounts);

        await _unitOfWork.SaveChangesAsync();

        foreach (var account in accounts)
            _eventBus.Publish(new AccountPeopleCreatedIntegrationEvent(account.Id, account.Name, account.Email));

        _logger.LogInformation("Account data seeded successfully!");
    }

    private static List<Account> GenerateAccounts()
    {
        var faker = new Faker<Account>()
            .RuleFor(account => account.Id, f => f.Random.Guid())
            .RuleFor(account => account.Name, f => f.Company.CompanyName())
            .RuleFor(account => account.Phone, f =>
            {
                var xeger = new Xeger(RegexPatterns.PhoneNumber);
                return xeger.Generate();
            })
            .RuleFor(account => account.Email, (f, account) => f.Internet.Email(account.Name))
            .RuleFor(account => account.Address, f => f.Address.FullAddress())
            .RuleFor(account => account.TotalSales, f => Math.Round(f.Random.Double(0, 1000000), 2))
            .RuleFor(account => account.CreatedDate, f => f.Date.Between(DateTime.Now, DateTime.Now.AddMonths(1)));

        return faker.Generate(50).ToList();
    }
}