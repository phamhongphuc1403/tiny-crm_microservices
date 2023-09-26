using BuildingBlock.Application.IntegrationEvents.Events;
using People.Domain.AccountAggregate.Entities;

namespace People.Application.IntegrationEvents.Events;

public record AccountsSeededIntegrationEvent : IntegrationEvent
{
    public IEnumerable<Account> Accounts;

    public AccountsSeededIntegrationEvent(IEnumerable<Account> accounts)
    {
        Accounts = accounts;
    }
}