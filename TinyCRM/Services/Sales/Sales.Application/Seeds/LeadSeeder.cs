using Bogus;
using BuildingBlock.Application;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Polly;
using Sales.Domain.AccountAggregate;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.Seeds;

public class LeadSeeder : IDataSeeder
{
    private readonly IReadOnlyRepository<Account> _accountReadonlyRepository;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadonlyRepository;
    private readonly ILogger<LeadSeeder> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public LeadSeeder(ILogger<LeadSeeder> logger, IOperationRepository<Lead> leadOperationRepository,
        IReadOnlyRepository<Lead> leadReadonlyRepository, IReadOnlyRepository<Account> accountReadonlyRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _leadOperationRepository = leadOperationRepository;
        _leadReadonlyRepository = leadReadonlyRepository;
        _accountReadonlyRepository = accountReadonlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedDataAsync()
    {
        var accountIds = new List<Guid>();
        var policy = Policy.Handle<SeedDataException>()
            .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Couldn't seed Leads table after {TimeOut}s", $"{time.TotalSeconds:n1}");
                }
            );

        policy.Execute(() =>
        {
            var accounts = _accountReadonlyRepository.GetAllAsync().Result;
            if (accounts.Count < 50) throw new SeedDataException("Account data not seeded yet!");

            accountIds = accounts.Select(account => account.Id).ToList();
        });

        if (await _leadReadonlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Leads data already seeded!");
            return;
        }

        SeedLeads(accountIds);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Lead data seeded successfully!");
    }

    private void SeedLeads(IEnumerable<Guid> accountIds)
    {
        var faker = new Faker<Lead>()
            .RuleFor(lead => lead.Id, f => f.Random.Guid())
            .RuleFor(lead => lead.Title, f => f.Lorem.Sentence())
            .RuleFor(lead => lead.Description, f => f.Lorem.Paragraph())
            .RuleFor(lead => lead.CustomerId, f => f.PickRandom(accountIds))
            .RuleFor(lead => lead.Source, f => f.PickRandom<LeadSource>())
            .RuleFor(lead => lead.EstimatedRevenue, f => f.Random.Double(0, 100000))
            .RuleFor(lead => lead.Status, f => f.PickRandom<LeadStatus>())
            .FinishWith((f, lead) =>
            {
                if (lead.Status == LeadStatus.Qualified)
                    lead.QualificationDate = f.Date.Past();

                if (lead.Status != LeadStatus.Disqualified) return;

                lead.DisqualificationReason = f.PickRandom<LeadDisqualificationReason>();
                lead.DisqualificationDescription = f.Lorem.Sentence();
                lead.DisqualificationDate = f.Date.Past();
            });

        _leadOperationRepository.AddRangeAsync(faker.Generate(500));
    }
}