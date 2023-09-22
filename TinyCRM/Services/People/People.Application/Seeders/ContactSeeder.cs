using Bogus;
using BuildingBlock.Application;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using People.Domain.AccountAggregate.Entities;

namespace People.Application.Seeders;

public class ContactSeeder : IDataSeeder
{
    private readonly IReadOnlyRepository<Account> _accountReadonlyRepository;
    private readonly IOperationRepository<Contact> _contactOperationRepository;
    private readonly IReadOnlyRepository<Contact> _contactReadonlyRepository;
    private readonly ILogger<ContactSeeder> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ContactSeeder(
        IOperationRepository<Contact> contactOperationRepository,
        IReadOnlyRepository<Contact> contactReadonlyRepository,
        IReadOnlyRepository<Account> accountReadonlyRepository,
        IUnitOfWork unitOfWork,
        ILogger<ContactSeeder> logger
    )
    {
        _contactOperationRepository = contactOperationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _contactReadonlyRepository = contactReadonlyRepository;
        _accountReadonlyRepository = accountReadonlyRepository;
    }

    public async Task SeedDataAsync()
    {
        if (await _contactReadonlyRepository.CheckIfExistAsync())
        {
            _logger.LogInformation("Contact data already seeded!");
            return;
        }

        var accountIds = (await _accountReadonlyRepository.GetAllAsync()).Select(account => account.Id);

        SeedContacts(accountIds);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Contact data seeded successfully!");
    }

    private void SeedContacts(IEnumerable<Guid> accountIds)
    {
        var faker = new Faker<Contact>()
            .RuleFor(contact => contact.Id, f => f.Random.Guid())
            .RuleFor(contact => contact.Name, f => f.Person.FullName)
            .RuleFor(contact => contact.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(contact => contact.Email, (f, contact) => f.Internet.Email(contact.Name))
            .RuleFor(contact => contact.CreatedDate, f => f.Date.Between(DateTime.Now, DateTime.Now.AddMonths(1)))
            .RuleFor(contact => contact.AccountId, f => f.PickRandom(accountIds));

        _contactOperationRepository.AddRangeAsync(faker.Generate(50));
    }
}