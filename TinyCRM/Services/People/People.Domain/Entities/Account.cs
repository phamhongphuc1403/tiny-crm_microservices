using BuildingBlock.Domain.Model;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Domain.Exceptions;
using People.Domain.Specifications;

namespace People.Domain.Entities;

public class Account : GuidEntity
{
    private Account(string name, string? email, string? phone, string? address)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public Account()
    {
    }

    public string Name { get; private set; } = null!;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public double TotalSales { get; private set; } = 0;

    public static async Task<Account> CreateAsync(string name, string? email, string? phone, string? address,
        IReadOnlyRepository<Account> repository)
    {
        if (email != null) await CheckIfExistAsync(email, repository);

        return new Account(name, email, phone, address);
    }

    private static async Task CheckIfExistAsync(string email, IReadOnlyRepository<Account> repository)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        Optional<bool>.Of(await repository.CheckIfExistAsync(accountEmailSpecification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email));
    }
}