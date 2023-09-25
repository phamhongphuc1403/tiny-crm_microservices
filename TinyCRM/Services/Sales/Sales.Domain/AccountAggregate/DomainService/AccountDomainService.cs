using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;

namespace Sales.Domain.AccountAggregate.DomainService;

public class AccountDomainService:IAccountDomainService
{
    private readonly IOperationRepository<Account> _operationRepository;
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;
    public AccountDomainService(IOperationRepository<Account> operationRepository, IReadOnlyRepository<Account> readOnlyRepository)
    {
        _operationRepository = operationRepository;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<Account> CreateAsync(string name, string? email=null)
    {
        await CheckValidOnCreateExistAsync(email);
        return new Account(name, email);
    }
    
    private async Task CheckValidOnCreateExistAsync(string? email)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(accountEmailSpecification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email??string.Empty));
    }
}