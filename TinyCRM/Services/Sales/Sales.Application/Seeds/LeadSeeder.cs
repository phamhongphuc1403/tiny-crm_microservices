using Bogus;
using BuildingBlock.Application;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Sales.Domain.AccountAggregate;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.Seeds;

public class LeadSeeder : IDataSeeder
{
    private readonly ILogger<LeadSeeder> _logger;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadonlyRepository;
    private readonly IReadOnlyRepository<Account> _accountReadonlyRepository;
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
        if (await _leadReadonlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Leads data already seeded!");
            return;
        }

        var accountIds = (await _accountReadonlyRepository.GetAllAsync()).Select(account => account.Id);

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
                if (lead.Status != LeadStatus.Disqualify) return;
                
                lead.DisqualificationReason = f.PickRandom<LeadDisqualificationReason>();
                lead.DisqualificationDescription = f.Lorem.Sentence();
                lead.DisqualificationDate = f.Date.Past();
            });

        _leadOperationRepository.AddRangeAsync(faker.Generate(500));
    }
}