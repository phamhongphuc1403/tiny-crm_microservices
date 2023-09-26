using BuildingBlock.Application.IntegrationEvents.Events;

namespace People.Application.IntegrationEvents.Events;

public record AccountCreatedOrUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid AccountId;
    public string? Email;
    public string Name;

    public AccountCreatedOrUpdatedIntegrationEvent(Guid id, string name, string? email)
    {
        AccountId = id;
        Name = name;
        Email = email;
    }
}