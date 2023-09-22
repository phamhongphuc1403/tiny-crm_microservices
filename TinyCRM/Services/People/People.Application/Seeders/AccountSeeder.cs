using Bogus;
using BuildingBlock.Application;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using People.Domain.AccountAggregate.Entities;

namespace People.Application.Seeders;

public class AccountSeeder : IDataSeeder
{
    private readonly ILogger<AccountSeeder> _logger;
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readonlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountSeeder(
        IOperationRepository<Account> operationRepository,
        IReadOnlyRepository<Account> readonlyRepository,
        IUnitOfWork unitOfWork,
        ILogger<AccountSeeder> logger
    )
    {
        _operationRepository = operationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _readonlyRepository = readonlyRepository;
    }

    public async Task SeedDataAsync()
    {
        if (await _readonlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Account data already seeded!");
            return;
        }

        SeedAccounts();
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Account data seeded successfully!");
    }

    private void SeedAccounts()
    {
        var faker = new Faker<Account>()
            .RuleFor(account => account.Id, f => f.Random.Guid())
            .RuleFor(account => account.Name, f => f.Company.CompanyName())
            .RuleFor(account => account.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(account => account.Email, (f, account) => f.Internet.Email(account.Name))
            .RuleFor(account => account.Address, f => f.Address.FullAddress())
            .RuleFor(account => account.TotalSales, f => Math.Round(f.Random.Double(0, 1000000), 2))
            .RuleFor(account => account.CreatedDate, f => f.Date.Between(DateTime.Now, DateTime.Now.AddMonths(1)));

        _operationRepository.AddRangeAsync(faker.Generate(50));
    }
}